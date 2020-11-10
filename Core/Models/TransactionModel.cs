using System;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class TransactionModel
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("sellAll")]
        public bool SellAll { get; set; }
        [JsonPropertyName("amount")]
        public float Amount { get; set; }
        [JsonPropertyName("userName")]
        public String UserName { get; set; }            
    }
}