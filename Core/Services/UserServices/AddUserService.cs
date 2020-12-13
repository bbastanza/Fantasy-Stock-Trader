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
        User AddUser(string userName, string password, string email);
    }

    public class AddUserService : IAddUserService
    {
        private readonly string _path;
        private readonly ISession _session;

        public AddUserService(INHibernateSessionService nHibernateSessionService)
        {
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        } 
        
        public User AddUser(string userName, string password, string email)
        {
            if (userName == null || password == null || email == null)
                throw new InvalidInputException(_path, "AddUser()");

            var user = _session.Query<User>().FirstOrDefault(x => x.UserName == userName);

            if (user != null)
                throw new ExistingUserException(_path, "AddUser()");
                
            var newUser = new User(userName, password, email);

            _session.Save(newUser);
            return newUser;
            
        }
    }
}