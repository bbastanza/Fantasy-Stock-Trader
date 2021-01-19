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
        private readonly IQueryDbService _queryDbService;

        public AddUserService(IQueryDbService queryDbService)
        {
            _queryDbService = queryDbService;
            _path = Path.GetFullPath(ToString());
        }

        public UserSession AddUser(string userName, string password, string email)
        {
            var user = _queryDbService.GetUser(userName);

            if (user != null)
                throw new ExistingUserException(_path, "AddUser()");

            var newUser = new User(userName, password, email);

            _queryDbService.SaveToDb(newUser);

            var userSession = new UserSession()
            {
                SessionId = Guid.NewGuid().ToString(),
                InitDateTime = DateTime.Now,
                ExpireDateTime = DateTime.Now.AddDays(1),
                User = newUser
            };

            _queryDbService.SaveToDb(userSession);

            return userSession;
        }
    }
}