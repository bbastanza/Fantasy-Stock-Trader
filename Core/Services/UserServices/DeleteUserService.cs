using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;
using NHibernate;
using NHibernate.Linq;

namespace Core.Services.UserServices
{
    public interface IDeleteUserService
    {
        string DeleteUser(string userName, string password);
    }
    public class DeleteUserService : IDeleteUserService
    {
        private readonly IDbQueryService _dbQueryService;
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;

        public DeleteUserService(
            IDbQueryService dbQueryService, 
            INHibernateSessionService nHibernateSessionService)
        {
            _dbQueryService = dbQueryService;
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }
        
        public string DeleteUser(string userName, string password)
        {
            if (userName == null || password == null)
                throw new InvalidInputException(Path.GetFullPath(ToString()), "DeleteUser");
            
            if (!_dbQueryService.ValidateUser(userName, password))
                throw new UserValidationException(_path, "DeleteUser()");
            
            DeleteUserFromDb(userName);
            
            return $"{userName} has been deleted from the database";
        }
        
        private async void DeleteUserFromDb(string userName)
        {
            var session = _nHibernateSessionService.GetSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.Query<User>()
                        .Where(user => user.UserName == userName).DeleteAsync();
                    await transaction.CommitAsync();
                }
            }
            catch
            {
                throw new DbInteractionException(_path, "DeleteUser()");
            }
            finally
            {
                _nHibernateSessionService.CloseSession();
            }
        } 
    }
}