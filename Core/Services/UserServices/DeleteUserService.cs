using System.IO;
using Core.Services.DbServices;
using Infrastructure.Exceptions;

namespace Core.Services.UserServices
{
    public interface IDeleteUserService
    {
        string DeleteUser(string userName, string password);
    }
    public sealed class DeleteUserService : IDeleteUserService
    {
        private readonly IDbQueryService _dbQueryService;
        private readonly IDbDeleteUserService _dbDeleteUserService;
        private readonly string _path;

        public DeleteUserService(IDbQueryService dbQueryService, IDbDeleteUserService dbDeleteUserService)
        {
            _dbQueryService = dbQueryService;
            _dbDeleteUserService = dbDeleteUserService;
            _path = Path.GetFullPath(ToString());
        }
        public string DeleteUser(string userName, string password)
        {
            if (userName == null || password == null)
                throw new InvalidInputException(Path.GetFullPath(ToString()), "DeleteUser");
            
            if (!_dbQueryService.ValidateUser(userName, password))
                throw new UserValidationException(_path, "DeleteUser()");
            
            _dbDeleteUserService.DeleteUser(userName);
            
            return $"{userName} has been deleted from the database";
        }
    }
}