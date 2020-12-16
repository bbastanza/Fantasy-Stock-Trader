using System.Text.Json.Serialization;

namespace API.Models
{
    public class SaleInputModel
    {
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }
        
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("shareAmount")] 
        public double ShareAmount { get; set; }

        [JsonPropertyName("sellAll")]
        public bool SellAll { get; set; }
    }
}
