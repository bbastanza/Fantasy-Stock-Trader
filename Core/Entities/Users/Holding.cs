using System.Text.Json.Serialization;
using Core.Entities.Transactions;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities.Users
{
    public class Holding
    {
        public Holding(Transaction transaction)
        {
            Symbol = transaction.Symbol;
            CompanyName = transaction.CompanyName;
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

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
