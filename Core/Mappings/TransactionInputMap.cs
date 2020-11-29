using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;


namespace Core.Mappings
{
    public interface ITransactionInputMap
    {
        Transaction MapInputTransaction(string type, double amount,string userName, IexStock iexData);
    }
    public class TransactionInputMap : ITransactionInputMap
    {
        // private ISelectUserService _selectUserService;
        
        public TransactionInputMap(/* ISelectUserService selectUserService*/)
        {

            // _selectUserService = selectUserService
        }

        public Transaction MapInputTransaction(string type, double amount,string userName, IexStock iexData)
        {
            if (amount <= 0 || userName == null)
                throw new InvalidInputException(Path.GetFullPath(ToString()) , "MapInputTransaction()");
            
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