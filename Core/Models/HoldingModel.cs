using System;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Core.Services;
using Newtonsoft.Json;


namespace Core.Models
{
    public class HoldingModel{
        
        [JsonPropertyName("stockModel")]
        public StockModel StockModel;
  
        public HoldingModel(string symbol)
        {
            StockModel = new StockModel(){Symbol = symbol};
        }
        
        [JsonPropertyName("value")] 
        public double Value { get; set; }

        [JsonPropertyName("totalShares")] 
        public double TotalShares { get; set; } = 200;


        public string GetSymbol()
        {
            return StockModel.Symbol;
        }
        
        public double SellAll(double currentPrice)
        {
            var totalShares = TotalShares;
            TotalShares = 0;
            return totalShares * currentPrice;
        }

        public void Sell(double sellShareAmount)
        {
            TotalShares -= sellShareAmount;
        }

        public void Purchase(double shareAmount)
        {
            TotalShares += shareAmount;
        }

        public double GetValue(double currentValue)
        {
            Value = TotalShares * currentValue;
            return Value;
        }

    }
}