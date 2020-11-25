using System;
using Core.Entities.Iex.IexServices;
using Core.Mappings;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface IHandleSaleService
    {
        Transaction SellTransaction(double amount, string userName, string symbol, bool sellAll = false);
    }

    public class HandleSaleService : IHandleSaleService
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly ISellShareService _sellShareService;
        private readonly ITransactionInputMap _transactionInputMap;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;

        public HandleSaleService(IIexFetchService iexFetchService, ISellShareService sellShareService,
            ITransactionInputMap transactionInputMap, ISetAllocatedFundsService setAllocatedFundsService,
            IStockListService stockListService)
        {
            _iexFetchService = iexFetchService;
            _sellShareService = sellShareService;
            _transactionInputMap = transactionInputMap;
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
        }

        public Transaction SellTransaction(double amount, string userName, string symbol, bool sellAll = false)
        {
            var transactionType = "sale";
            var iexData = _iexFetchService.GetStockBySymbol(symbol);
            var transaction = _transactionInputMap.MapInputTransaction(transactionType, amount, userName, iexData);
            transaction.User = _sellShareService.SellShares(transaction, sellAll);
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