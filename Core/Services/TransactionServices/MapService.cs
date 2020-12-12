using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.TransactionServices
{
    public interface IMapService
    {
        Transaction MapInputTransaction(string type, double amount,string userName, IexStock iexData, bool sellAll = false);
    }
    
    public class MapService : IMapService
    {
        private readonly string _path;
        private readonly ISession _session;

        public MapService(INHibernateSessionService nHibernateSessionService)
        {
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public Transaction MapInputTransaction(string type, double amount, string userName, IexStock iexData, bool sellAll = false)
        {
            if (userName == null || (amount <= 0 && !sellAll))
                throw new InvalidInputException(_path, "MapInputTransaction()");
            
            var transaction = new Transaction
            {
                Type = type,
                Amount = amount,
                Symbol = iexData.Symbol,
                SellAll = sellAll,
                CompanyName = iexData.CompanyName,
                TransactionPrice = iexData.LatestPrice,
                User = new User(userName, "password", "email")
            };

            return transaction;
        }
    }
}