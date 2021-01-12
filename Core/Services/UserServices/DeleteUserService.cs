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
        private readonly IQueryDb _queryDb;
        private readonly string _path;

        public DeleteUserService(IQueryDb queryDb)
        {
            _queryDb = queryDb;
            _path = Path.GetFullPath(ToString());
        }

        public string DeleteUser(string userName, string password)
        {
            var user = _queryDb.GetUser(userName);

            if (user.Password != password)
                throw new UserValidationException(_path, "DeleteUser()");
            
            _queryDb.DeleteUser(userName);

            return $"{userName} has been deleted from the database";
        }
    }
}