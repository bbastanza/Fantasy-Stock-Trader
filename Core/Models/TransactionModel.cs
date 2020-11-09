using System;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class TransactionModel
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("amount")]
        public double Amount { get; set; }
        [JsonPropertyName("userName")]
        public String UserName { get; set; }            
    }
}