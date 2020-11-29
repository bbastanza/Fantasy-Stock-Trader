using System;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities
{
    public class Transaction
    {
        public virtual string Type { get; set; }
        public virtual string Symbol { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual double Amount { get; set; }
        public virtual double CurrentPrice { get; set; }
        public virtual DateTime CreatedAt { get; } = DateTime.Now;
        public virtual User User { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}