using System;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;



namespace Core.Models
{
    public class HoldingModel{
        
        public HoldingModel(TransactionModel transactionModel)
        {
            Symbol = transactionModel.Symbol;
            CompanyName = transactionModel.CompanyName;
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