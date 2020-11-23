using System;
using System.Text.Json.Serialization;

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
    }
}