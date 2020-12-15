using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        private readonly ISession _session;
        private readonly string _path;

        public LoginUser(INHibernateSession nHibernateSession)
        {
            _session = nHibernateSession.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public UserSession Login(string userName, string password)
        {
            var user = _session.Query<User>()
                .FirstOrDefault(x => x.UserName == userName);
            
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

            _session.Save(userSession);
            
            return userSession;
        }
    }
}