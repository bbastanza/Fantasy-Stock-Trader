using System.IO;
using System.Linq;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;
using NHibernate.Linq;
using Transaction = Core.Entities.Transaction;

namespace Core.Services.DbServices
{
    public interface IDbHandleSale
    {
        void DbSale(Transaction transaciton);
    }
    
    public class DbHandleSale : IDbHandleSale
    {
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly IDbAddTransactionService _dbAddTransactionService;
        private readonly string _path;
        private readonly ISession _session;

        public DbHandleSale(
            INHibernateSessionService nHibernateSessionService,
            IDbAddTransactionService dbAddTransactionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _dbAddTransactionService = dbAddTransactionService;
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public void DbSale(Transaction transaction)
        {
            if (transaction.SellAll) RemoveHolding(transaction.Holding);
            else UpdateHolding(transaction.Holding);

            _dbAddTransactionService.AddTransaction(transaction);
        }

        private async void RemoveHolding(Holding holding)
        {
            try
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.Query<Holding>()
                        .Where(x => x.Symbol == holding.Symbol).DeleteAsync();
                    await transaction.CommitAsync();
                }
            }
            catch
            {
                throw new DbInteractionException(_path, "RemoveHolding");
            }
            finally
            {
                _nHibernateSessionService.CloseSession();
            }
        }

        private async void UpdateHolding(Holding holding)
        {
            try
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveOrUpdateAsync(holding);
                    await transaction.CommitAsync();
                }

            }
            catch
            {
                throw new DbInteractionException(_path, "UpdateHolding()");
            }
            finally
            {
                _nHibernateSessionService.CloseSession();
            }
        }
    }
}