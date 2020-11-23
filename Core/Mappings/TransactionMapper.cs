using System;
using Core.Entities.Iex;
using Core.Entities.Transactions;
using Core.Entities.Users;

namespace Core.Mappings
{
    public interface ITransactionMapper
    {
        TransactionEntity MapTransaction(string type, double amount,string userName, IexStockModel iexData);
    }
    public class TransactionMapper : ITransactionMapper
    {
        // private ISelectUserService _selectUserService;
        
        public TransactionMapper(/* ISelectUserService selectUserService*/)
        {
            // _selectUserService = selectUserService
        }

        public TransactionEntity MapTransaction(string type, double amount,string userName, IexStockModel iexData)
        {
            var transaction = new TransactionEntity
            {
                Type = type,
                Amount = amount,
                Symbol = iexData.Symbol,
                CompanyName = iexData.CompanyName,
                CurrentPrice = iexData.LatestPrice,
                UserEntity = new UserEntity(userName,"Password", "joey@baggs.com")
                // User = _selectUserService.GetUserByName(transactionInput.UserName)
            };

            return transaction;
        }
    }
}