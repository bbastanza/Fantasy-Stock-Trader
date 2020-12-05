using System.IO;
using System.Linq;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;
using NHibernate.Linq;

namespace Core.Services.DbServices
{
    public interface IDbDeleteUserService
    {
        void DeleteUser(string userName);
    }
    public class DbDeleteUserService : IDbDeleteUserService
    {
        private readonly INHibernateSessionService _nHibernateSessionService;
        private string _path;

        public DbDeleteUserService(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }

        public async void DeleteUser(string userName)
        {
            var session = _nHibernateSessionService.GetSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.Query<User>()
                        .Where(user => user.UserName == userName).DeleteAsync();
                    await transaction.CommitAsync();
                }
            }
            catch
            {
                throw new DbInteractionException(_path, "DeleteUser()");
            }
            finally
            {
                _nHibernateSessionService.CloseSession();
            }
        } 
    }
}
