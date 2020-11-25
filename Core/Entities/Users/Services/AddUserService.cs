using System.IO;

namespace Core.Entities.Users.Services
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
                throw new InvalidDataException(
                    $"The data received is incomplete\nUserName: {userName}\nPassword: {password}\nEmail: {email}");

            // if checkUserService.CheckUserByUserName(username) returns null
            var newUser = new User(userName, password, email);
            // add newUser to db
            return newUser;
        }
    }
}