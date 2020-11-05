using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class UserData
    {

        [JsonPropertyName("username")]
        public string UserName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

        public UserData()
        {
  
        }
    }
}