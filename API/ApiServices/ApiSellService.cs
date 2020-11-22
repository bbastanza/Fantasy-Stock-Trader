using API.Mappings;
using API.Models;
using Core.Entities.Transactions;
using Core.Entities.Transactions.TransactionServices;
using Core.Services;

namespace API.ApiServices
{
    public interface IApiSellService
    {
        Transaction SellTransaction(TransactionInputModel transactionInput);
    }
    
    public class ApiSellService : IApiSellService
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly ISellShareService _sellShareService;
        private readonly ITransactionMapper _transactionMapper;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;

        public ApiSellService(IIexFetchService iexFetchService, ISellShareService sellShareService,
            ITransactionMapper transactionMapper, ISetAllocatedFundsService setAllocatedFundsService, IStockListService stockListService)
        {
            _iexFetchService = iexFetchService;
            _sellShareService = sellShareService;
            _transactionMapper = transactionMapper;
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
        }

        public Transaction SellTransaction(TransactionInputModel transactionInput)
        {
            var iexData = _iexFetchService.GetStockBySymbol(transactionInput.Symbol);
            var transaction = _transactionMapper.MapTransaction(transactionInput, iexData);
            transaction.User = _sellShareService.SellShares(transaction, transactionInput.SellAll);
            transaction.User.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(
                    _stockListService.GetStockModelList(transaction.User),
                    transaction.User.Holdings
                );
            return transaction;
        }
    }
}