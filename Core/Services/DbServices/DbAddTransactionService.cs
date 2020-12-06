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
        private ISession _session;
        private string _path;

        public DbAddTransactionService(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        }
        
        
        public async void AddTransaction(Transaction stockTransaction)
        {
            try
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveAsync(stockTransaction);
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