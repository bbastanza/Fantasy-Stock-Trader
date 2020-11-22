using API.Models;
using Core.Entities.Transactions;
using Core.Entities.Transactions.TransactionServices;
using Core.Entities.Users;
using Core.Entities.Users.Services;
using Core.Models;

namespace API.Mappings
{
    public interface ITransactionMapper
    {
        Transaction MapTransaction(TransactionInputModel transactionInput, IexStockModel iexData);
    }
    public class TransactionMapper : ITransactionMapper
    {
        private readonly IStockListService _stockListService;

        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        // private ISelectUserService _selectUserService;
        
        public TransactionMapper(/* ISelectUserService selectUserService*/IStockListService stockListService, ISetAllocatedFundsService setAllocatedFundsService)
        {
            _stockListService = stockListService;
            _setAllocatedFundsService = setAllocatedFundsService;
            // _selectUserService = selectUserService
        }

        public Transaction MapTransaction(TransactionInputModel transactionInput, IexStockModel iexData)
        {
            var transaction = new Transaction
            {
                Amount = transactionInput.PurchaseAmount,
                SellAll = transactionInput.SellAll,
                Symbol = iexData.Symbol,
                CompanyName = iexData.CompanyName,
                CurrentPrice = iexData.LatestPrice,
                User = new User("Brian","Password")
                // User = _selectUserService.GetUserByName(transactionInput.UserName)
            };
            transaction.User.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(
                    _stockListService.GetStockModelList(transaction.User),
                    transaction.User.Holdings
                    );
            return transaction;
        }
    }
}