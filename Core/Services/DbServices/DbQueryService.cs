using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;

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
        private readonly string _path;
        private readonly ISession _session;

        public DbQueryService(INHibernateSessionService nHibernateSessionService)
        {
            _path = Path.GetFullPath(ToString());
            _session = nHibernateSessionService.GetSession();
        }

        public bool CheckExistingUser(string userName)
        {
            var potentialUser = _session.Query<User>()
                .SingleOrDefault(user => user.UserName == userName);
            return potentialUser == null;
        }

        public User GetUserFromDb(string userName)
        {
            var currentUser = _session.Query<User>()
                .SingleOrDefault(user => user.UserName == userName);

            if (currentUser == null)
                throw new NonExistingUserException(_path, "GetUserFromDb()");

            return currentUser;
        }

        public List<Transaction> GetUserTransactions(int userId)
        {
            return _session.Query<Transaction>()
                .Where(x => x.User.Id == userId).ToList();
        }

        public List<Holding> GetUserHoldings(int userId)
        {
             return _session.Query<Holding>()
                    .Where(x => x.UserId == userId).ToList();
        }

        public bool ValidateUser(string userName, string password)
        {
            var currentUser = _session.Query<User>()
                .SingleOrDefault(user => user.UserName == userName);
            return currentUser != null && currentUser.Password == password;
        }
    }
}