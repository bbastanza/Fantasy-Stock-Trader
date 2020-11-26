using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Entities.Transactions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities.Users
{
    public class User
    {
        public User(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
        }

        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual double Balance { get; set; } = 100000;
        public virtual double AllocatedFunds { get; set; }
        public virtual List<Holding> Holdings { get; set; } = new List<Holding>()
            {new Holding(new Transaction() {CompanyName = "Caterpillar", Symbol = "CAT"}) {TotalShares = 30}};

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}