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
        void DbSale(Transaction transaction);
    }
    
    public class DbHandleSale : IDbHandleSale
    {
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly IDbAddTransactionService _dbAddTransactionService;
        private readonly IDbUpdateHolding _dbUpdateHolding;
        private readonly string _path;

        public DbHandleSale(
            INHibernateSessionService nHibernateSessionService,
            IDbAddTransactionService dbAddTransactionService,
            IDbUpdateHolding dbUpdateHolding)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _dbAddTransactionService = dbAddTransactionService;
            _dbUpdateHolding = dbUpdateHolding;
            _path = Path.GetFullPath(ToString());
        }

        public void DbSale(Transaction transaction)
        {
            if (transaction.SellAll) RemoveHolding(transaction.Holding);
            else _dbUpdateHolding.UpdateHolding(transaction.Holding);
            _dbAddTransactionService.AddTransaction(transaction);
        }

        private async void RemoveHolding(Holding holding)
        {
            var session = _nHibernateSessionService.GetSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.Query<Holding>()
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
    }
}