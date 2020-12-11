using System;
using System.Collections.Generic;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities
{
    public class User : EntityBase
    {
        public User()
        {
            // Holdings = new List<Holding>();
            // Transactions = new List<Transaction>();
        }

        public User(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
            Balance = 100000;
        }

        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual double Balance { get; set; } 
        public virtual double AllocatedFunds { get; set; }
        public virtual IList<Transaction> Transactions { get; set; }
        public virtual IList<Holding> Holdings { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}