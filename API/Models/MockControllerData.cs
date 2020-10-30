using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class MockControllerData
    {

        [JsonPropertyName("username")]
        public string UserName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

        public MockControllerData(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}