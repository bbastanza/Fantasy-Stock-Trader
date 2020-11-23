using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Entities.Transactions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities.Users
{
    public class UserEntity
    {
        public UserEntity(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
        }

        [JsonPropertyName("userName")]
        public virtual string UserName { get; set; }
        [JsonPropertyName("password")]
        public virtual string Password { get; set; }
        [JsonPropertyName("email")]
        public virtual string Email { get; set; }
        [JsonPropertyName("createdAt")] 
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonPropertyName("unallocatedDollars")]
        public virtual double UnallocatedFunds { get; set; } = 100000;
        [JsonPropertyName("allocatedDollars")]
        public virtual double AllocatedFunds { get; set; }
        [JsonPropertyName("holdings")]
        public virtual List<HoldingEntity> Holdings { get; set; } = new List<HoldingEntity>()
            {new HoldingEntity(new TransactionEntity() {CompanyName = "Caterpillar", Symbol = "CAT"}) {TotalShares = 30}};

        public string ReadHolding(string symbol)
        {
            foreach (var holding in Holdings)
                if (holding.Symbol == symbol)
                    return JsonSerializer.Serialize(holding);

            return "Holding does not exist for this user";
        }
    }
}