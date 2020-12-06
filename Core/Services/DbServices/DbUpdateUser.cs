using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface IDbUpdateUser
    {
        void Update(User user);
    }
    public class DbUpdateUser : IDbUpdateUser
    {
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;
        private readonly ISession _session;

        public DbUpdateUser(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public async void Update(User user)
        {
            try
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.UpdateAsync(user);
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