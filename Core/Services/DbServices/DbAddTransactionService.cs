using System;
using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface IDbAddTransactionService
    {
        void AddTransaction(Transaction stockTransaction);
    }
    
    public class DbAddTransactionService : IDbAddTransactionService
    {
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;

        public DbAddTransactionService(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }
        
        
        public async void AddTransaction(Transaction stockTransaction)
        {
            
            var session = _nHibernateSessionService.GetSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.SaveAsync(stockTransaction);
                    await transaction.CommitAsync();
                }
            }
            catch 
            {
                throw new DbInteractionException(_path, "AddTransaction()");
            }
            finally
            {
                _nHibernateSessionService.CloseSession();
            }
        }
    }
}