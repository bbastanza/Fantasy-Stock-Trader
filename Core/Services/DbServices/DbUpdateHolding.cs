using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface IDbUpdateHolding
    {
        void UpdateHolding(Holding holding);
    }
    
    public class DbUpdateHolding : IDbUpdateHolding
    {
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;

        public DbUpdateHolding(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }
        
        public async void UpdateHolding(Holding holding)
        {
            var session = _nHibernateSessionService.GetSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.UpdateAsync(holding);
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