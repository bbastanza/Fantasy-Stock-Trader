using API.Mappings;
using API.Models;
using Core.Entities.Transactions;
using Core.Entities.Transactions.TransactionServices;
using Core.Services;

namespace API.ApiServices
{
    public interface IApiPurchaseService
    {
        Transaction PurchaseTransaction(TransactionInputModel transactionInput);
    }
    
    public class ApiPurchaseService : IApiPurchaseService
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly IPurchaseSharesService _purchaseSharesService;
        private readonly ITransactionMapper _transactionMapper;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;

        public ApiPurchaseService(IIexFetchService iexFetchService, IPurchaseSharesService purchaseSharesService,
            ITransactionMapper transactionMapper, ISetAllocatedFundsService setAllocatedFundsService, IStockListService stockListService)
        {
            _iexFetchService = iexFetchService;
            _purchaseSharesService = purchaseSharesService;
            _transactionMapper = transactionMapper;
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
        }

        public Transaction PurchaseTransaction(TransactionInputModel transactionInput)
        {
            var iexData = _iexFetchService.GetStockBySymbol(transactionInput.Symbol);
            var transaction = _transactionMapper.MapTransaction(transactionInput, iexData);
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