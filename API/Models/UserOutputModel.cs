using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Entities.Users;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class UserOutputModel
    {
        [JsonPropertyName("userName")] 
        public string UserName { get; set; }
        
        [JsonPropertyName("balance")] 
        public double Balance { get; set; }
        
        [JsonPropertyName("allocatedFunds")] 
        public double AllocatedFunds { get; set; }
        
        [JsonPropertyName("holdings")] 
        public List<Holding> Holdings { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}