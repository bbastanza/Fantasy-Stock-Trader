using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface IDbHandlePurchase
    {
        void DbPurchase(Transaction transaction);
    }
    
    public class DbHandlePurchase : IDbHandlePurchase
    {
        private readonly IDbAddTransactionService _addTransactionService;
        private readonly IDbUpdateHolding _updateHolding;
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;

        public DbHandlePurchase(
            IDbAddTransactionService addTransactionService,
            IDbUpdateHolding updateHolding,
            INHibernateSessionService nHibernateSessionService)
        {
            _addTransactionService = addTransactionService;
            _updateHolding = updateHolding;
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }

        public void DbPurchase(Transaction transaction)
        {
            if (transaction.NewHolding)
                AddNewHolding(transaction.Holding);
            else 
                _updateHolding.UpdateHolding(transaction.Holding);
                
            _addTransactionService.AddTransaction(transaction);
        }

        private async void AddNewHolding(Holding holding)
        {
            var session = _nHibernateSessionService.GetSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.SaveAsync(holding);
                    await transaction.CommitAsync();
                }
            }
            catch
            {
                throw new DbInteractionException(_path,"AddUser()");
            }
            finally
            {
                _nHibernateSessionService.CloseSession();
            }
        }
    }
}