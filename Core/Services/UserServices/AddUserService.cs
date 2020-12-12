using System.IO;
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
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;
        private ISession _session;

        public AddUserService(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        } 
        
        public User AddUser(string userName, string password, string email)
        {
            if (userName == null || password == null || email == null)
                throw new InvalidInputException(_path, "AddUser()");

            // if (!_dbQueryService.CheckExistingUser(userName))
            //     throw new ExistingUserException(_path, "AddUser()");
            
            var newUser = new User(userName, password, email);

            AddUserToDb(newUser);
            
            return newUser;
        }

        private async void AddUserToDb(User user)
        {
            
            try
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveAsync(user);
                    await transaction.CommitAsync();
                }
            }
            catch
            {
                throw new DbInteractionException(_path, "UpdateHolding");
            }
            finally
            {
                _nHibernateSessionService.CloseSession();
            }
        }
    }
}