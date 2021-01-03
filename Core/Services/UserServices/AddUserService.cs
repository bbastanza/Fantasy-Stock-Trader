using System;
using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.UserServices
{
    public interface IAddUserService
    {
        UserSession AddUser(string userName, string password, string email);
    }

    public class AddUserService : IAddUserService
    {
        private readonly string _path;
        private readonly ISession _session;

        public AddUserService(INHibernateSession nHibernateSession)
        {
            _session = nHibernateSession.GetSession();
            _path = Path.GetFullPath(ToString());
        } 
        
        public UserSession AddUser(string userName, string password, string email)
        {
            var user = _session.Query<User>().FirstOrDefault(x => x.UserName == userName);

            if (user != null)
                throw new ExistingUserException(_path, "AddUser()");
                
            var newUser = new User(userName, password, email);

            _session.Save(newUser);

            var userSession = new UserSession()
            {
                SessionId = Guid.NewGuid().ToString(),
                InitDateTime = DateTime.Now,
                ExpireDateTime = DateTime.Now.AddDays(1),
                User = newUser
            };
            
            _session.Save(userSession);
            
            return userSession;
            
        }
    }
}
