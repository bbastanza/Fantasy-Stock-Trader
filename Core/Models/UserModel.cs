using System.Collections.Generic;

namespace Core.Models
{
    public class UserModel
    {
        public string UserName { get; set; } = "Brian";
        public string Password { get; set; } = "Password";
        public float UnallocatedDollars { get; set; } = 90000;
        public float AllocatedDollars { get; set; } = 10000;
        public List<HoldingModel> Holdings { get; set; } = new List<HoldingModel>(){new HoldingModel(),new HoldingModel()};
    }
}