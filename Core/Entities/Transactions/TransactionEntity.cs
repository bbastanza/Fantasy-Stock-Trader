using System;
using System.Text.Json.Serialization;
using Core.Entities.Users;

namespace Core.Entities.Transactions
{
    public class TransactionEntity
    {
        [JsonPropertyName("type")]
        public virtual string Type { get; set; }
        [JsonPropertyName("symbol")]
        public virtual string Symbol { get; set; }
        [JsonPropertyName("companyName")]
        public virtual string CompanyName { get; set; }
        [JsonPropertyName("amount")]
        public virtual double Amount { get; set; }
        [JsonPropertyName("purchasePrice")] 
        public virtual double CurrentPrice { get; set; }
        [JsonPropertyName("createdAt")] 
        public virtual DateTime CreatedAt { get; } = DateTime.Now;
        [JsonPropertyName("user")]
        public virtual UserEntity UserEntity { get; set; }
    }
}