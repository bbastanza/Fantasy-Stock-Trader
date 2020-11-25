using Core.Entities.Iex;
using Core.Entities.Transactions;
using Core.Entities.Users;

namespace Core.Mappings
{
    public interface ITransactionInputMap
    {
        Transaction MapInputTransaction(string type, double amount,string userName, IexStockModel iexData);
    }
    public class TransactionInputMap : ITransactionInputMap
    {
        // private ISelectUserService _selectUserService;
        
        public TransactionInputMap(/* ISelectUserService selectUserService*/)
        {
            // _selectUserService = selectUserService
        }

        public Transaction MapInputTransaction(string type, double amount,string userName, IexStockModel iexData)
        {
            var transaction = new Transaction
            {
                Type = type,
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