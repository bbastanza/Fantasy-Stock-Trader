using API.Models;
using Core.Entities.Users;

namespace API.OutputMappings
{
    public interface IUserOutputMap
    {
        UserModel MapUserOutput(User user);
    }

    public class UserOutputMap : IUserOutputMap
    {
        public UserModel MapUserOutput(User user)
        {
            var outputModel = new UserModel()
            {
                UserName = user.UserName,
                Balance = user.Balance,
                AllocatedFunds = user.AllocatedFunds,
                Holdings = user.Holdings
            };

            return outputModel;
        }
    }
}