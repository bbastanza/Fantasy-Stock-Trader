using API.Models;
using Core.Entities;
using Core.Entities.Transactions;
using Core.Entities.Transactions.TransactionServices;
using Core.Entities.Users;

namespace API.Mappings
{
    public interface ITransactionMapper
    {
        Transaction MapTransaction(TransactionInputModel transactionInput, IexStockModel iexData);
    }
    public class TransactionMapper : ITransactionMapper
    {
        // private ISelectUserService _selectUserService;
        
        public TransactionMapper(/* ISelectUserService selectUserService*/)
        {
            // _selectUserService = selectUserService
        }

        public Transaction MapTransaction(TransactionInputModel transactionInput, IexStockModel iexData)
        {
            var transaction = new Transaction
            {
                Amount = transactionInput.Amount,
                Symbol = iexData.Symbol,
                CompanyName = iexData.CompanyName,
                CurrentPrice = iexData.LatestPrice,
                User = new User(transactionInput.UserName,"Password", "joey@baggs.com")
                // User = _selectUserService.GetUserByName(transactionInput.UserName)
            };

            return transaction;
        }
    }
}