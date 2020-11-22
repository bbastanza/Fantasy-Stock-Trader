using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Entities.Transactions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities.Users
{
    public class User
    {
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        [JsonPropertyName("userName")]
        public virtual string UserName { get; set; }
        [JsonPropertyName("password")]
        public virtual string Password { get; set; }
        [JsonPropertyName("createdAt")] 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonPropertyName("unallocatedDollars")]
        public virtual double UnallocatedFunds { get; set; } = 100000;
        [JsonPropertyName("allocatedDollars")]
        public virtual double AllocatedFunds { get; set; }
        [JsonPropertyName("holdings")]
        public virtual List<Holding> Holdings { get; set; } = new List<Holding>()
            {new Holding(new Transaction() {CompanyName = "Caterpillar", Symbol = "CAT"}) {TotalShares = 30}};

        public string ReadHolding(string symbol)
        {
            foreach (var holding in Holdings)
                if (holding.Symbol == symbol)
                    return JsonSerializer.Serialize(holding);

            return "Holding does not exist for this user";
        }
    }
}