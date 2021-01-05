using System.Collections.Generic;
using Core.Entities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class UserModel
    {
        public UserModel(User user)
        {
            UserName = user.UserName;
            Balance = user.Balance;
            AllocatedFunds = user.AllocatedFunds;
            
            foreach (var holding in user.Holdings)
            {
                Holdings.Add(new HoldingModel(holding));   
            }
        }
        
        public string UserName { get; }
        public double Balance { get; }
        public double AllocatedFunds { get; }
        public IList<HoldingModel> Holdings { get; } = new List<HoldingModel>(); 
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}