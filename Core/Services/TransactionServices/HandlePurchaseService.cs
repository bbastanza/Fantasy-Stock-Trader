using System;
using Core.Entities;
using Core.Mappings;
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
        private readonly ITransactionInputMap _transactionInputMap;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;

        public HandlePurchaseService(IIexFetchService iexFetchService, IPurchaseSharesService purchaseSharesService,
            ITransactionInputMap transactionInputMap, ISetAllocatedFundsService setAllocatedFundsService, IStockListService stockListService)
        {
            _iexFetchService = iexFetchService;
            _purchaseSharesService = purchaseSharesService;
            _transactionInputMap = transactionInputMap;
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
        }

        public Transaction PurchaseTransaction(double amount, string userName,string symbol)
        {
            var transactionType = "purchase";
            var iexData = _iexFetchService.GetStockBySymbol(symbol);
            var transaction = _transactionInputMap.MapInputTransaction(transactionType, amount, userName, iexData);
            transaction.User = _purchaseSharesService.PurchaseShares(transaction);
            transaction.User.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(
                    _stockListService.GetStockModelList(transaction.User),
                    transaction.User.Holdings
                );
            Console.WriteLine(transaction);
            return transaction;
        }
    }
}