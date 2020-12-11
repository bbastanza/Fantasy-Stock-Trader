using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using NHibernate;
using NHibernate.Linq;

namespace Core.Services.TransactionServices
{
    public interface IHandleSaleService
    {
        Transaction SellTransaction(double amount, string userName, string symbol, bool sellAll = false);
    }

    public class HandleSaleService : IHandleSaleService
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly ISellShareService _sellShareService;
        private readonly IMapService _mapService;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;
        private readonly IDbUpdateService _updateService;
        private readonly IDbAddService _dbAddService;
        private readonly IDbUpdateService _dbUpdateService;
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;

        public HandleSaleService(
            IIexFetchService iexFetchService,
            ISellShareService sellShareService,
            IMapService mapService, 
            ISetAllocatedFundsService setAllocatedFundsService,
            IStockListService stockListService,
            IDbUpdateService updateService,
            IDbAddService dbAddService,
            IDbUpdateService dbUpdateService,
            INHibernateSessionService nHibernateSessionService)
        {
            _iexFetchService = iexFetchService;
            _sellShareService = sellShareService;
            _mapService = mapService;
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
            _updateService = updateService;
            _dbAddService = dbAddService;
            _dbUpdateService = dbUpdateService;
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }

        public Transaction SellTransaction(double amount, string userName, string symbol, bool sellAll = false)
        {
            var transactionType = "sale";
            
            var iexData = _iexFetchService.GetStockBySymbol(symbol);
            
            var transaction = _mapService.MapInputTransaction(transactionType, amount, userName, iexData, sellAll);
            
            transaction.User = _sellShareService.SellShares(transaction, sellAll);
            
            transaction.User.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(
                    _stockListService.GetStockModelList(transaction.User),
                    transaction.User.Holdings
                );
            
            transaction.Holding.UserId = transaction.User.Id;
            
            DbSale(transaction);
            
            _updateService.Update(transaction.User);
            
            return transaction;
        }

        private void DbSale(Transaction transaction)
        {
            if (transaction.SellAll) RemoveHolding(transaction.Holding);
            else _dbUpdateService.Update(transaction.Holding);
            _dbAddService.Add(transaction);
        }
        
        private async void RemoveHolding(Holding holding)
        {
            var session = _nHibernateSessionService.GetSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.Query<Holding>()
                        .Where(x => x.Symbol == holding.Symbol).DeleteAsync();
                    await transaction.CommitAsync();
                }
            }
            catch
            {
                throw new DbInteractionException(_path, "RemoveHolding");
            }
            finally
            {
                _nHibernateSessionService.CloseSession();
            }
        }
    }
}