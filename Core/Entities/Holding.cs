namespace Core.Entities
{
    public class Holding : EntityBase
    {
        public Holding()
        {
        }
        
        public virtual string Symbol { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual double Value { get; set; }
        public virtual double TotalShares { get; set; }
        public virtual User User { get; set; }

        public virtual double SellAll()
        {
            var sharedToSell = TotalShares;
            TotalShares = 0;
            return sharedToSell;
        }

        public virtual bool SellAndReturnIsOverdrawn(double sellShareAmount)
        {
            TotalShares -= sellShareAmount;
            return TotalShares < 0;
        }

        public virtual void Purchase(double shareAmount)
        {
            TotalShares += shareAmount;
        }

        public virtual void SetValue(double currentValue)
        {
            Value = TotalShares * currentValue;
        }
    }
}
