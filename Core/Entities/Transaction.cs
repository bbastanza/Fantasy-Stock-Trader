using System;

namespace Core.Entities
{
    public class Transaction : EntityBase
    {
        public Transaction()
        {
        }
        
        public virtual string Type { get; set; }
        public virtual double Amount { get; set; }
        public virtual double TransactionPrice { get; set; }
        public virtual DateTime TransactionDate { get; set; } 
        public virtual bool SellAll { get; set; }
        public virtual User User { get; set; }
        public virtual Holding Holding { get; set; }
    }
}