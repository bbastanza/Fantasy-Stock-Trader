using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class UserInputModel
    {
        [JsonPropertyName("userName")] 
        public string UserName { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }    
        
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}