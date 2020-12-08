using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class PurchaseInputModel
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}