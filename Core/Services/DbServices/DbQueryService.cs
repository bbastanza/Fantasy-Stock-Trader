using System;
using System.IO;
using System.Linq;
using Core.Entities;
using Infrastructure.Exceptions;

namespace Core.Services.DbServices
{
    public interface IDbQueryService
    {
        bool CheckExistingUser(string userName);
        bool ValidateUser(string userName, string password);
        User GetUserFromDb(string userName, string password);
    }
    public class DbQueryService : IDbQueryService
    {
        private readonly INHibernateSessionService _nHibernateSessionService;
        private string _path;

        public DbQueryService(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }

        public bool CheckExistingUser(string userName)
        {
            var session = _nHibernateSessionService.GetSession();
            var potentialUser = session.Query<User>()
                .SingleOrDefault(user => user.UserName == userName);
            _nHibernateSessionService.CloseSession();
            return (potentialUser == null);
        }

        public User GetUserFromDb(string userName, string password)
        {
            var session = _nHibernateSessionService.GetSession();
            
            var currentUser = session.Query<User>()
                .SingleOrDefault(user => user.UserName == userName);
            _nHibernateSessionService.CloseSession();
            
            if (currentUser == null)
                throw new NonExistingUserException(_path, "GetUserFromDb()");
            
            if (currentUser.Password != password)
                throw new UserValidationException(_path, "GetUserFromDb()");

            return currentUser;
        }

        public bool ValidateUser(string userName, string password)
        {
            var session = _nHibernateSessionService.GetSession();
            var currentUser = session.Query<User>()
                .SingleOrDefault(user => user.UserName == userName);
            return currentUser != null && currentUser.Password == password;
        }
    }
}