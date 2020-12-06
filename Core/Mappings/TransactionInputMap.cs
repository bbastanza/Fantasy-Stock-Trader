using System.IO;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;

namespace Core.Mappings
{
    public interface ITransactionInputMap
    {
        Transaction MapInputTransaction(string type, double amount,string userName, IexStock iexData, bool sellAll = false);
    }
    
    public class TransactionInputMap : ITransactionInputMap
    {
        private readonly IDbQueryService _dbQueryService;
        private readonly string _path;

        public TransactionInputMap(IDbQueryService dbQueryService)
        {
            _dbQueryService = dbQueryService;
            _path = Path.GetFullPath(ToString());
        }

        public Transaction MapInputTransaction(string type, double amount,string userName, IexStock iexData, bool sellAll = false)
        {
            if (amount <= 0 || userName == null)
                throw new InvalidInputException(_path, "MapInputTransaction()");
            
            var transaction = new Transaction
            {
                Type = type,
                Amount = amount,
                Symbol = iexData.Symbol,
                SellAll = sellAll,
                CompanyName = iexData.CompanyName,
                TransactionPrice = iexData.LatestPrice,
                User = _dbQueryService.GetUserFromDb(userName)
            };
            
            transaction.User.Holdings = _dbQueryService.GetUserHoldings(transaction.User.Id);

            return transaction;
        }
    }
}