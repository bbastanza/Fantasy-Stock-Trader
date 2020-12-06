using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;
using NHibernate.Linq;

namespace Core.Services.DbServices
{
    public interface IDbQueryService
    {
        bool CheckExistingUser(string userName);
        bool ValidateUser(string userName, string password);
        User GetUserFromDb(string userName);
        List<Holding> GetUserHoldings(int userId);
    }

    public class DbQueryService : IDbQueryService
    {
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;
        private ISession _session;

        public DbQueryService(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public bool CheckExistingUser(string userName)
        {
            var potentialUser = _session.Query<User>()
                .SingleOrDefault(user => user.UserName == userName);
            _nHibernateSessionService.CloseSession();
            return potentialUser == null;
        }

        public User GetUserFromDb(string userName)
        {
            var currentUser = _session.Query<User>()
                .SingleOrDefault(user => user.UserName == userName);
            _nHibernateSessionService.CloseSession();

            if (currentUser == null)
                throw new NonExistingUserException(_path, "GetUserFromDb()");

            return currentUser;
        }

        public List<Holding> GetUserHoldings(int userId)
        {
             var holdings = _session.Query<Holding>()
                    .Where(x => x.UserId == userId).ToList();

            _nHibernateSessionService.CloseSession();

            return holdings;
        }

        public bool ValidateUser(string userName, string password)
        {
            var currentUser = _session.Query<User>()
                .SingleOrDefault(user => user.UserName == userName);
            return currentUser != null && currentUser.Password == password;
        }
    }
}