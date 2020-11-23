using System;
using System.Text.Json.Serialization;
using Core.Entities.Users;

namespace Core.Entities.Transactions
{
    public class Transaction
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
        [JsonPropertyName("amount")]
        public double Amount { get; set; }
        [JsonPropertyName("purchasePrice")] 
        public double PurchasePrice { get; set; }
        [JsonPropertyName("user")]
        public User User { get; set; }
        [JsonPropertyName("createdAt")] 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}