using API.Models;
using Core.Entities.Users;

namespace API.OutputMappings
{
    public interface IUserOutputMap
    {
        UserOutputModel MapUserOutput(User user);
    }

    public class UserOutputMap : IUserOutputMap
    {
        public UserOutputModel MapUserOutput(User user)
        {
            var outputModel = new UserOutputModel()
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