using Core.Entities;
using Core.Entities.Transactions;
using Core.Entities.Users;

namespace Core.Mappings
{
    public interface ITransactionMapper
    {
        Transaction MapTransaction(double amount,string userName, IexStockModel iexData);
    }
    public class TransactionMapper : ITransactionMapper
    {
        // private ISelectUserService _selectUserService;
        
        public TransactionMapper(/* ISelectUserService selectUserService*/)
        {
            // _selectUserService = selectUserService
        }

        public Transaction MapTransaction(double amount,string userName, IexStockModel iexData)
        {
            var transaction = new Transaction
            {
                Amount = amount,
                Symbol = iexData.Symbol,
                CompanyName = iexData.CompanyName,
                CurrentPrice = iexData.LatestPrice,
                User = new User(userName,"Password", "joey@baggs.com")
                // User = _selectUserService.GetUserByName(transactionInput.UserName)
            };

            return transaction;
        }
    }
}