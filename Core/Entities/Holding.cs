using System.IO;
using Infrastructure.Exceptions;

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

        public virtual double SellAllReturnShareAmount()
        {
            var sharedToSell = TotalShares;
            TotalShares = 0;
            return sharedToSell;
        }

        public virtual void Sell(double sellShareAmount)
        {
            TotalShares -= sellShareAmount;
            if (TotalShares < 0)
                throw new OverDrawnHoldingException(Path.GetFullPath(ToString()), "Sell()");
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
