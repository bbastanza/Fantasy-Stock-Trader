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
        private StockModel _stockModel;
  
        public HoldingModel()
        {
            _stockModel = new StockModel();
        }
        
        [JsonPropertyName("value")] 
        private double Value { get; set; }

        [JsonPropertyName("totalShares")] 
        private double TotalShares { get; set; } = 200;

        public void PurchaseShares(float purchaseAmount,double currentPrice)
        {
            var purchaseShareAmount = purchaseAmount / currentPrice;
            // saves price that it was last purchase at
            _stockModel.IexRealtimePrice = currentPrice;
            TotalShares += purchaseShareAmount;
            Value = TotalShares * currentPrice;
        }

        public void SellShares(float saleAmount, double currentPrice, bool sellAll = false)
        {
            if (sellAll)
            {
                
            }
      
            var sellShareAmount = saleAmount / currentPrice;
            _stockModel.IexRealtimePrice = currentPrice;
            TotalShares -= sellShareAmount;
            Value = TotalShares * currentPrice;
        }

        public string ReadHolding()
        {
            return JsonSerializer.Serialize($"Value: {Value}," +
                                            $" TotalShares: {TotalShares}," +
                                            $" Symbol: {_stockModel.Symbol}," +
                                            $" CompanyName: {_stockModel.CompanyName}," +
                                            $" Price: {_stockModel.IexRealtimePrice}");
        }
    }
}