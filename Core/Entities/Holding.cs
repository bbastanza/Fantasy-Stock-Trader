using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities
{
    public class Holding : EntityBase
    {
        public Holding()
        {
            
        }
        public Holding(Transaction transaction)
        {
            Symbol = transaction.Symbol;
            CompanyName = transaction.CompanyName;
        }

        public virtual string Symbol { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual double Value { get; set; }
        public virtual double TotalShares { get; set; }
        public virtual User User { get; set; }

        public virtual double SellAll(double currentPrice)
        {
            var sharedToSell = TotalShares;
            TotalShares = 0;
            return sharedToSell * currentPrice;
        }

        public virtual void Sell(double sellShareAmount)
        {
            TotalShares -= sellShareAmount;
        }

        public virtual void Purchase(double shareAmount)
        {
            TotalShares += shareAmount;
        }

        public virtual void SetValue(double currentValue)
        {
            Value = TotalShares * currentValue;
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
