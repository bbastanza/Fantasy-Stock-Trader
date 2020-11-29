using System.Collections.Generic;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class UserModel
    {
        [JsonPropertyName("userName")] 
        public string UserName { get; set; }
        
        [JsonPropertyName("balance")] 
        public double Balance { get; set; }
        
        [JsonPropertyName("allocatedFunds")] 
        public double AllocatedFunds { get; set; }
        
        [JsonPropertyName("holdings")]
        public List<HoldingModel> Holdings { get; set; } = new List<HoldingModel>();

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}