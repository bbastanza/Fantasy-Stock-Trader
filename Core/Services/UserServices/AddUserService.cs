using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;

namespace Core.Services.UserServices
{
    public interface IAddUserService
    {
        User AddUser(string userName, string password, string email);
    }

    public class AddUserService : IAddUserService
    {
        public User AddUser(string userName, string password, string email)
        {
            if (userName == null || password == null || email == null)
                throw new InvalidInputException(Path.GetFullPath(ToString()), "AddUser()");

            // if checkUserService.CheckUserByUserName(username) returns null
            var newUser = new User(userName, password, email);
            // add newUser to db
            return newUser;
        }
    }
}