using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Services;

namespace Core.Models
{
    public class UserModel
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; } 
        [JsonPropertyName("password")]
        public string Password { get; set; } 
        [JsonPropertyName("unallocatedDollars")]
        public double UnallocatedDollars { get; set; } = 100000;
        [JsonPropertyName("allocatedDollars")]
        public double AllocatedDollars { get; set; }
        [JsonPropertyName("holdings")]
        public List<HoldingModel> Holdings { get; set; } = new List<HoldingModel>(){new HoldingModel()};


        public void AdjustAllocation(double transactionAmount)
        {
            UnallocatedDollars -= transactionAmount;
            AllocatedDollars += transactionAmount;
        }
    }
}