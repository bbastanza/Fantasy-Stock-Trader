using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class IexStock
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
        
        [JsonPropertyName("latestPrice")]
        public double LatestPrice { get; set; }
    }
}