using Core.Entities;
using Core.Services.DbServices;
using Core.Services.IexServices;

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
        private readonly IDbUpdateService _dbUpdateService;
        private readonly IDbAddService _dbAddService;

        public HandlePurchaseService(
            IIexFetchService iexFetchService, 
            IPurchaseSharesService purchaseSharesService,
            IMapService mapService, 
            ISetAllocatedFundsService setAllocatedFundsService,
            IStockListService stockListService,
            IDbUpdateService dbUpdateService,
            IDbAddService dbAddService)
        {
            _iexFetchService = iexFetchService;
            _purchaseSharesService = purchaseSharesService;
            _mapService = mapService;
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
            _dbUpdateService = dbUpdateService;
            _dbAddService = dbAddService;
        }

        public Transaction PurchaseTransaction(double amount, string userName, string symbol)
        {
            var transactionType = "purchase";
            
            var iexData = _iexFetchService.GetStockBySymbol(symbol);
            
            var transaction = _mapService.MapInputTransaction(transactionType, amount, userName, iexData);
            
            transaction.User = _purchaseSharesService.PurchaseShares(transaction);
            
            transaction.User.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(
                    _stockListService.GetStockModelList(transaction.User),
                    transaction.User.Holdings
                );
            
            transaction.Holding.UserId = transaction.User.Id;
            
            DbPurchase(transaction);
            
            _dbUpdateService.Update(transaction.User);
            
            return transaction;
        }
        
        private void DbPurchase(Transaction transaction)
        {
            if (transaction.NewHolding)
                _dbAddService.Add(transaction.Holding);
            else 
                _dbUpdateService.Update(transaction.Holding);
                
            _dbAddService.Add(transaction);
        }
    }
}