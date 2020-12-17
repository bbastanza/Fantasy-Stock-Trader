using System.Text.Json.Serialization;

namespace API.Models
{
    public class SessionInputModel
    {
        [JsonPropertyName("sessionId")] 
        public string SessionId { get; set; }
    }
}