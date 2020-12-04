using System;
using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface IDbAddService
    {
        void AddUser(User user);
    }
    public class DbAddService : IDbAddService
    {
        private readonly INHibernateSessionService _nHibernateSessionService;

        public DbAddService(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
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
                throw new DbInteractionException(Path.GetFullPath(ToString()), "AddUser()");
            }
            finally
            {
                _nHibernateSessionService.CloseSession();
            }
        } 
    }
}