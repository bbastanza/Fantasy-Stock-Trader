using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface IDbUpdateService
    {
        void Update(EntityBase entity);
    }
    public class DbUpdateService : IDbUpdateService
    {
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;

        public DbUpdateService(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }

        public async void Update(EntityBase entity)
        {
            var session = _nHibernateSessionService.GetSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.UpdateAsync(entity);
                    await transaction.CommitAsync();
                }
            }
            catch
            {
                throw new DbInteractionException(_path, "UpdateBalance()");
            }
            finally
            {
                _nHibernateSessionService.CloseSession();
            }
        }
    }
}