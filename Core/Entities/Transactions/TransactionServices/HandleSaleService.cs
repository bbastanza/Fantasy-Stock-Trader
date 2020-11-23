using System;
using Core.Entities.Iex.IexServices;
using Core.Mappings;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface IHandleSaleService
    {
        TransactionEntity SellTransaction(double amount, string userName, string symbol, bool sellAll = false);
    }
    
    public class HandleSaleService : IHandleSaleService
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly ISellShareService _sellShareService;
        private readonly ITransactionMapper _transactionMapper;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;

        public HandleSaleService(IIexFetchService iexFetchService, ISellShareService sellShareService,
            ITransactionMapper transactionMapper, ISetAllocatedFundsService setAllocatedFundsService, IStockListService stockListService)
        {
            _iexFetchService = iexFetchService;
            _sellShareService = sellShareService;
            _transactionMapper = transactionMapper;
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
        }

        public TransactionEntity SellTransaction(double amount, string userName, string symbol, bool sellAll = false)
        {
            var transactionType = "sale";
            var iexData = _iexFetchService.GetStockBySymbol(symbol);
            var transaction = _transactionMapper.MapTransaction(transactionType, amount, userName, iexData);
            transaction.UserEntity = _sellShareService.SellShares(transaction, sellAll);
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