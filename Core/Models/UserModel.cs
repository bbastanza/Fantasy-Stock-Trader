using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class UserModel
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; } 
        [JsonPropertyName("password")]
        public string Password { get; set; } 
        [JsonPropertyName("unallocatedDollars")]
        public float UnallocatedDollars { get; set; } = 100000;
        [JsonPropertyName("allocatedDollars")]
        public float AllocatedDollars { get; set; }
        [JsonPropertyName("holdings")]
        public List<HoldingModel> Holdings { get; set; } = new List<HoldingModel>(){new HoldingModel()};
    }
}