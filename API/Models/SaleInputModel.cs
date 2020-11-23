using System;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class SaleInputModel
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("amount")] 
        public double Amount { get; set; }

        [JsonPropertyName("sellAll")]
        public bool SellAll { get; set; }
        
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
    }
}