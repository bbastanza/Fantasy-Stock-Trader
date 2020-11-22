using System.Text.Json.Serialization;
using Core.Entities.Users;
using Core.Models;

namespace Core.Entities.Transactions
{
    public class Transaction
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
        [JsonPropertyName("currentPrice")]
        public double CurrentPrice { get; set; }
        [JsonPropertyName("sellAll")]
        public bool SellAll { get; set; }
        [JsonPropertyName("amount")]
        public double Amount { get; set; }
        [JsonPropertyName("user")]
        public User User { get; set; }            
    }
}