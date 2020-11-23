using System.Text.Json.Serialization;
using Core.Entities.Transactions;

namespace Core.Entities.Users
{
    public class HoldingEntity
    {
        public HoldingEntity(TransactionEntity transactionEntity)
        {
            Symbol = transactionEntity.Symbol;
            CompanyName = transactionEntity.CompanyName;
        }

        [JsonPropertyName("symbol")]
        public virtual string Symbol { get; set; }
        
        [JsonPropertyName("companyName")]
        public virtual string CompanyName { get; set; }
        
        [JsonPropertyName("value")]
        public virtual double Value { get; set; }
        
        [JsonPropertyName("totalShares")]
        public virtual double TotalShares { get; set; }

        public double SellAll(double currentPrice)
        {
            var sharedToSell = TotalShares;
            TotalShares = 0;
            return sharedToSell * currentPrice;
        }

        public void Sell(double sellShareAmount)
        {
            TotalShares -= sellShareAmount;
        }

        public void Purchase(double shareAmount)
        {
            TotalShares += shareAmount;
        }

        public void SetValue(double currentValue)
        {
            Value = TotalShares * currentValue;
        }
    }
}
