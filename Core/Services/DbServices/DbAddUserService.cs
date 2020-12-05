using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface IDbAddUserService
    {
        void AddUser(User user);
    }
    public class DbAddUserService : IDbAddUserService
    {
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;

        public DbAddUserService(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }

        public async void AddUser(User user)
        {
            var session = _nHibernateSessionService.GetSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.SaveAsync(user);
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