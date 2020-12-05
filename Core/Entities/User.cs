using System;
using System.Collections.Generic;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
        }

        public virtual string UserName { get; }
        public virtual string Password { get; }
        public virtual string Email { get; }
        public virtual DateTime CreatedAt { get; } = DateTime.Now;
        public virtual double Balance { get; set; } = 100000;
        public virtual double AllocatedFunds { get; set; }
        public virtual List<Holding> Holdings { get; } = new List<Holding>();    

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}