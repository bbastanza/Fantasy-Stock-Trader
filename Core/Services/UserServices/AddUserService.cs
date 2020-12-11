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
        private readonly IDbAddService _dbAddService;
        private readonly string _path;

        public AddUserService(IDbQueryService dbQueryService, IDbAddService dbAddService)
        {
            _dbQueryService = dbQueryService;
            _dbAddService = dbAddService;
            _path = Path.GetFullPath(ToString());
        } 
        
        public User AddUser(string userName, string password, string email)
        {
            if (userName == null || password == null || email == null)
                throw new InvalidInputException(_path, "AddUser()");

            if (!_dbQueryService.CheckExistingUser(userName))
                throw new ExistingUserException(_path, "AddUser()");
            
            var newUser = new User(userName, password, email);
            
            _dbAddService.Add(newUser);
            
            return newUser;
        }
    }
}