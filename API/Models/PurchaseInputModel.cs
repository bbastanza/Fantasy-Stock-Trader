using System.Text.Json.Serialization;

namespace API.Models
{
    public class PurchaseInputModel
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        
        [JsonPropertyName("amount")]
        public double Amount { get; set; }
        
        [JsonPropertyName("userName")]
        public string UserName { get; set; } 
    }
}