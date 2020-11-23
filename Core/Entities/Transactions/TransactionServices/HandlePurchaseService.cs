using Core.Entities.Iex.IexServices;
using Core.Mappings;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface IHandlePurchaseService
    {
        Transaction PurchaseTransaction(double amount, string symbol, string userName);
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

        public Transaction PurchaseTransaction(double amount, string userName,string symbol)
        {
            var iexData = _iexFetchService.GetStockBySymbol(symbol);
            var transaction = _transactionMapper.MapTransaction(amount,userName , iexData);
            transaction.User = _purchaseSharesService.PurchaseShares(transaction);
            transaction.User.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(
                    _stockListService.GetStockModelList(transaction.User),
                    transaction.User.Holdings
                );
            return transaction;
        }
    }
}