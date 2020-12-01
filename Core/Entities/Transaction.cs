using System;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities
{
    public class Transaction
    {
        public Transaction()
        {
            
        }
        public virtual int Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string Symbol { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual double Amount { get; set; }
        public virtual double TransactionPrice { get; set; }
        public virtual DateTime TransactionDate { get; } = DateTime.Now;
        public virtual User User { get; set; }
        public virtual Holding Holding { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}