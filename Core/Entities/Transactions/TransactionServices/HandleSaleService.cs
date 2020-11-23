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

        public Transaction SellTransaction(double amount, string userName, string symbol, bool sellAll = false)
        {
            var iexData = _iexFetchService.GetStockBySymbol(symbol);
            var transaction = _transactionMapper.MapTransaction(amount, userName, iexData);
            transaction.User = _sellShareService.SellShares(transaction, sellAll);
            transaction.User.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(
                    _stockListService.GetStockModelList(transaction.User),
                    transaction.User.Holdings
                );
            return transaction;
        }
    }
}