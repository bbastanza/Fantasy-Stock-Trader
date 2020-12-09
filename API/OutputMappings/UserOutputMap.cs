using System.Collections.Generic;
using API.Models;
using Core.Entities;

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
            };
            if (user.Holdings != null)
            {
                foreach (var holding in user.Holdings)
                {
                    outputModel.Holdings.Add(new HoldingModel(holding));
                }
            }

            return outputModel;
        }
    }
}