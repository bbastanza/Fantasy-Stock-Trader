using Core.Entities;
using Core.Mappings;
using Core.Services.DbServices;
using Core.Services.IexServices;

namespace Core.Services.TransactionServices
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
        private readonly IDbHandleSale _dbHandleSale;
        private readonly IDbUpdateUser _updateUser;

        public HandleSaleService(
            IIexFetchService iexFetchService,
            ISellShareService sellShareService,
            ITransactionInputMap transactionInputMap, 
            ISetAllocatedFundsService setAllocatedFundsService,
            IStockListService stockListService,
            IDbHandleSale dbHandleSale,
            IDbUpdateUser updateUser)
        {
            _iexFetchService = iexFetchService;
            _sellShareService = sellShareService;
            _transactionInputMap = transactionInputMap;
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
            _dbHandleSale = dbHandleSale;
            _updateUser = updateUser;
        }

        public Transaction SellTransaction(double amount, string userName, string symbol, bool sellAll = false)
        {
            var transactionType = "sale";
            
            var iexData = _iexFetchService.GetStockBySymbol(symbol);
            
            var transaction = _transactionInputMap.MapInputTransaction(transactionType, amount, userName, iexData, sellAll);
            
            transaction.User = _sellShareService.SellShares(transaction, sellAll);
            
            transaction.User.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(
                    _stockListService.GetStockModelList(transaction.User),
                    transaction.User.Holdings
                );
            
            _dbHandleSale.DbSale(transaction);
            
            _updateUser.Update(transaction.User);
            
            return transaction;
        }
    }
}