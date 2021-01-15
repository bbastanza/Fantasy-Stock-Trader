using System.IO;
using Core.Services.DbServices;
using Infrastructure.Exceptions;

namespace Core.Services.UserServices
{
    public interface IDeleteUserService
    {
        string DeleteUser(string userName, string password);
    }

    public class DeleteUserService : IDeleteUserService
    {
        private readonly IQueryDbService _queryDbService;
        private readonly string _path;

        public DeleteUserService(IQueryDbService queryDbService)
        {
            _queryDbService = queryDbService;
            _path = Path.GetFullPath(ToString());
        }

        public string DeleteUser(string userName, string password)
        {
            var user = _queryDbService.GetUser(userName);

            if (user.Password != password)
                throw new UserValidationException(_path, "DeleteUser()");
            
            _queryDbService.DeleteUser(userName);

            return $"{userName} has been deleted from the database";
        }
    }
}