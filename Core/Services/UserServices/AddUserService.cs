using System.IO;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;

namespace Core.Services.UserServices
{
    public interface IAddUserService
    {
        User AddUser(string userName, string password, string email);
    }

    public class AddUserService : IAddUserService
    {
        private readonly IDbQueryService _dbQueryService;
        private readonly IDbAddUserService _dbAddUserService;

        public AddUserService(IDbQueryService dbQueryService, IDbAddUserService dbAddUserService)
        {
            _dbQueryService = dbQueryService;
            _dbAddUserService = dbAddUserService;
        } 
        public User AddUser(string userName, string password, string email)
        {
            if (userName == null || password == null || email == null)
                throw new InvalidInputException(Path.GetFullPath(ToString()), "AddUser()");

            if (!_dbQueryService.CheckExistingUser(userName))
                throw new ExistingUserException(Path.GetFullPath(ToString()), "AddUser()");
            
            var newUser = new User(userName, password, email);
            _dbAddUserService.AddUser(newUser);
            
            return newUser;
        }
    }
}