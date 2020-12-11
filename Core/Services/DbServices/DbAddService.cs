using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface IDbAddService
    {
        void Add(EntityBase entity);
    }
    public class DbAddService : IDbAddService
    {
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;

        public DbAddService(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }

        public async void Add(EntityBase entity)
        {
            var session = _nHibernateSessionService.GetSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.SaveAsync(entity);
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