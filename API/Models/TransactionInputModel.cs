using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class TransactionInputModel
    {
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }
        
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("amount")] 
        public double Amount { get; set; }

        [JsonPropertyName("sellAll")]
        public bool SellAll { get; set; }
        
    }
}