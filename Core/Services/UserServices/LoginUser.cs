using System;
using System.IO;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;

namespace Core.Services.UserServices
{
    public interface ILoginService
    {
        UserSession Login(string userName, string password);
        void Logout(string sessionId);
    } 
    
    public class LoginService : ILoginService
    {
        private readonly IQueryDb _queryDb;
        private readonly string _path;

        public LoginService(IQueryDb queryDb)
        {
            _queryDb = queryDb;
            _path = Path.GetFullPath(ToString());
        }

        public UserSession Login(string userName, string password)
        {
            var user = _queryDb.GetUser(userName); 
            
            if (user == null)
                throw new NonExistingUserException(_path, "Login()");
            
            if (user.Password != password)
                throw new UserValidationException(_path, "Login()");
            
            var userSession = new UserSession()
            {
                SessionId = Guid.NewGuid().ToString(),
                InitDateTime = DateTime.Now,
                ExpireDateTime = DateTime.Now.AddDays(1),
                User = user
            };

            _queryDb.SaveToDb(userSession);
            
            return userSession;
        }

        public void Logout(string sessionId)
        {
            _queryDb.DeleteSession(sessionId);
        }
    }
}