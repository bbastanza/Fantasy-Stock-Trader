namespace Core.Entities.Users.Services
{
    public interface IAddUserService
    {
        UserEntity AddUser(string userName, string password, string email);
    }
    public class AddUserService : IAddUserService
    {
        public UserEntity AddUser(string userName, string password, string email)
        {
            // if checkUserService.CheckUserByUserName(username) returns null
            var newUser = new UserEntity(userName,password, email);
            // add newUser to db
            return newUser;
        }
    }
}