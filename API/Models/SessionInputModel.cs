using System.Text.Json.Serialization;

namespace API.Models
{
    public class SessionInputModel
    {
        [JsonPropertyName("id")] 
        public string Id { get; set; }
    }
}