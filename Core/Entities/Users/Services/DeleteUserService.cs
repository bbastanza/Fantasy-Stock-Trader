namespace Core.Entities.Users.Services
{
    public interface IDeleteUserService
    {
        string DeleteUser(string userName, string password);
    }
    public class DeleteUserService : IDeleteUserService
    {
        public string DeleteUser(string userName, string password)
        {
            // if _checkCredentialsService.CheckUser(userName, password)
            //     delete user from db
            // maybe add to logging table
            return $"{userName} has been deleted from the database";
        }
    }
}