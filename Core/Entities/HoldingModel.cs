using System.Text.Json.Serialization;
using Core.Entities.Transactions;

namespace Core.Models
{
    public class HoldingModel
    {
        public HoldingModel(Transaction transaction)
        {
            Symbol = transaction.Symbol;
            CompanyName = transaction.CompanyName;
        }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
        
        [JsonPropertyName("value")]
        public double Value { get; set; }
        
        [JsonPropertyName("totalShares")]
        public double TotalShares { get; set; }

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
