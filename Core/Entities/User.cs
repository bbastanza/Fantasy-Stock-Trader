using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class User : EntityBase
    {
        public User()
        {
        }
        
        public User(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
            Balance = 100000;
            Transactions = new List<Transaction>();
            Holdings = new List<Holding>();
            CreatedAt = DateTime.Now;
        }

        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime CreatedAt { get; set; } 
        public virtual double Balance { get; set; } 
        public virtual double AllocatedFunds { get; set; }
        public virtual IList<Transaction> Transactions { get; set; }
        public virtual IList<Holding> Holdings { get; set; }
    }
}