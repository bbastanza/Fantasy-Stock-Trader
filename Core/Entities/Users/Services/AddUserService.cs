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
            // if checkUserService.CheckUserByUserName(username) returns null
            var newUser = new User(userName,password, email);
            // add newUser to db
            return newUser;
        }
    }
}