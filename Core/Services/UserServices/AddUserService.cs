using System;
using System.IO;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;

namespace Core.Services.UserServices
{
    public interface IAddUserService
    {
        UserSession AddUser(string userName, string password, string email);
    }


    public class AddUserService : IAddUserService
    {
        private readonly string _path;
        private readonly IQueryDb _queryDb;

        public AddUserService(IQueryDb queryDb)
        {
            _queryDb = queryDb;
            _path = Path.GetFullPath(ToString());
        }

        public UserSession AddUser(string userName, string password, string email)
        {
            var user = _queryDb.GetUser(userName);

            if (user != null)
                throw new ExistingUserException(_path, "AddUser()");

            var newUser = new User(userName, password, email);

            _queryDb.SaveToDb(newUser);

            var userSession = new UserSession()
            {
                SessionId = Guid.NewGuid().ToString(),
                InitDateTime = DateTime.Now,
                ExpireDateTime = DateTime.Now.AddDays(1),
                User = newUser
            };

            _queryDb.SaveToDb(userSession);

            return userSession;
        }
    }
}