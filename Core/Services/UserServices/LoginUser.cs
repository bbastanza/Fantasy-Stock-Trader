using System;
using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.UserServices
{
    public interface ILoginUser
    {
        UserSession Login(string userName, string password);
    } 
    
    public class LoginUser : ILoginUser
    {
        private readonly IQueryDb _queryDb;
        private readonly string _path;

        public LoginUser(IQueryDb queryDb)
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
    }
}