using System;
using Core.Entities.Iex.IexServices;
using Core.Mappings;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface IHandlePurchaseService
    {
        TransactionEntity PurchaseTransaction(double amount, string symbol, string userName);
    }
    
    public class HandlePurchaseService : IHandlePurchaseService
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly IPurchaseSharesService _purchaseSharesService;
        private readonly ITransactionMapper _transactionMapper;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;

        public HandlePurchaseService(IIexFetchService iexFetchService, IPurchaseSharesService purchaseSharesService,
            ITransactionMapper transactionMapper, ISetAllocatedFundsService setAllocatedFundsService, IStockListService stockListService)
        {
            _iexFetchService = iexFetchService;
            _purchaseSharesService = purchaseSharesService;
            _transactionMapper = transactionMapper;
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
        }

        public TransactionEntity PurchaseTransaction(double amount, string userName,string symbol)
        {
            var transactionType = "purchase";
            var iexData = _iexFetchService.GetStockBySymbol(symbol);
            var transaction = _transactionMapper.MapTransaction(transactionType, amount, userName, iexData);
            transaction.UserEntity = _purchaseSharesService.PurchaseShares(transaction);
            transaction.UserEntity.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(
                    _stockListService.GetStockModelList(transaction.UserEntity),
                    transaction.UserEntity.Holdings
                );
            Console.WriteLine(JsonSerializer.Serialize(transaction));
            return transaction;
        }
    }
}