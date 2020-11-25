using System;
using System.Text.Json.Serialization;
using Core.Entities.Users;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities.Transactions
{
    public class Transaction
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
        public virtual User User { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}