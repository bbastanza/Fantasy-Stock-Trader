using System.IO;
using Infrastructure.Exceptions;

namespace Core.Services.UserServices
{
    public interface IDeleteUserService
    {
        string DeleteUser(string userName, string password);
    }
    public class DeleteUserService : IDeleteUserService
    {
        public string DeleteUser(string userName, string password)
        {
            if (userName == null || password == null)
                throw new InvalidInputException(Path.GetFullPath(ToString()), "DeleteUser");
            // if _checkCredentialsService.CheckUser(userName, password)
            //     delete user from db
            // maybe add to logging table
            return $"{userName} has been deleted from the database";
        }
    }
}