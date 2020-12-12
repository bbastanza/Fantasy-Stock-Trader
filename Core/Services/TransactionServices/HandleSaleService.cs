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
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;
        private readonly ISession _session;

        public HandleSaleService(
            IIexFetchService iexFetchService,
            ISellShareService sellShareService,
            IMapService mapService, 
            ISetAllocatedFundsService setAllocatedFundsService,
            IStockListService stockListService,
            INHibernateSessionService nHibernateSessionService)
        {
            _iexFetchService = iexFetchService;
            _sellShareService = sellShareService;
            _mapService = mapService;
            _setAllocatedFundsService = setAllocatedFundsService;
            _nHibernateSessionService = nHibernateSessionService;
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public Transaction SellTransaction(double amount, string userName, string symbol, bool sellAll = false)
        {
            var transactionType = "sale";
            
            var iexData = _iexFetchService.GetStockBySymbol(symbol);
            
            var transaction = _mapService.MapInputTransaction(transactionType, amount, userName, iexData, sellAll);
            
            transaction.User = _sellShareService.SellShares(transaction, sellAll);
            
            transaction.User.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(transaction.User);
            
            transaction.Holding.UserId = transaction.User.Id;
            
            DbSale(transaction);
            
            // _updateService.Update(transaction.User);
            
            return transaction;
        }

        private void DbSale(Transaction transaction)
        {
            if (transaction.SellAll) RemoveHolding(transaction.Holding);
            else UpdateHolding(transaction.Holding);
            // _dbAddService.Add(transaction);
        }

        private async void UpdateHolding(Holding holding)
        {
            try
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.Query<Holding>()
                        .UpdateAsync(x => x.Symbol == holding.Symbol);
                    await transaction.CommitAsync();
                }
            }
            catch
            {
                throw new DbInteractionException(_path, "UpdateHolding");
            }
            finally
            {
                _nHibernateSessionService.CloseSession();
            }
        }
        
        private async void RemoveHolding(Holding holding)
        {
            try
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.Query<Holding>()
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