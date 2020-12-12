using System.IO;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using NHibernate;
using NHibernate.Linq;

namespace Core.Services.TransactionServices
{
    public interface IHandlePurchaseService
    {
        Transaction PurchaseTransaction(double amount, string symbol, string userName);
    }

    public class HandlePurchaseService : IHandlePurchaseService
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly IPurchaseSharesService _purchaseSharesService;
        private readonly IMapService _mapService;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly ISession _session;
        private string _path;

        public HandlePurchaseService(
            IIexFetchService iexFetchService, 
            IPurchaseSharesService purchaseSharesService,
            IMapService mapService, 
            ISetAllocatedFundsService setAllocatedFundsService,
            IStockListService stockListService,
            INHibernateSessionService nHibernateSessionService)
        {
            _iexFetchService = iexFetchService;
            _purchaseSharesService = purchaseSharesService;
            _mapService = mapService;
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
            _nHibernateSessionService = nHibernateSessionService;
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public Transaction PurchaseTransaction(double amount, string userName, string symbol)
        {
            var transactionType = "purchase";
            
            var iexData = _iexFetchService.GetStockBySymbol(symbol);
            
            var transaction = _mapService.MapInputTransaction(transactionType, amount, userName, iexData);
            
            transaction.User = _purchaseSharesService.PurchaseShares(transaction);

            transaction.User.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(transaction.User);
            
            DbPurchase(transaction);
            
            // _dbUpdateService.Update(transaction.User);
            
            return transaction;
        }
        
        private void DbPurchase(Transaction transaction)
        {
            if (transaction.NewHolding)
                AddHolding(transaction.Holding);
            else 
                UpdateHolding(transaction.Holding);
                
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
        
        private async void AddHolding(Holding holding)
        {
            try
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveAsync(holding);
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