using System.Text.Json.Serialization;

namespace API.Models
{
    public class PurchaseInputModel
    {
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }
        
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("amount")] 
        public double Amount { get; set; }
    }
}