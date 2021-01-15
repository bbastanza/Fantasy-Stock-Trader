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
        private readonly IQueryDbService _queryDbService;
        private readonly string _path;

        public LoginService(IQueryDbService queryDbService)
        {
            _queryDbService = queryDbService;
            _path = Path.GetFullPath(ToString());
        }

        public UserSession Login(string userName, string password)
        {
            var user = _queryDbService.GetUser(userName); 
            
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

            _queryDbService.SaveToDb(userSession);
            
            return userSession;
        }

        public void Logout(string sessionId)
        {
            _queryDbService.DeleteSession(sessionId);
        }
    }
}