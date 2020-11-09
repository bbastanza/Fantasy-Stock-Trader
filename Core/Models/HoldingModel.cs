using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Core.Services;


namespace Core.Models
{
    public class HoldingModel{
        
        private StockModel _stockModel;
        private double _totalShares = 200;


        public HoldingModel()
        {
            _stockModel = new StockModel();
        }
        
        [JsonPropertyName("value")]
        public double Value { get; set; }
        [JsonPropertyName("totalShares")]
        public double TotalShares{ get; set; }

        public void PurchaseShares(double purchaseAmount,double currentPrice)
        {
            var shareAmount = purchaseAmount / currentPrice;
            _totalShares += shareAmount;
            Value = _totalShares * currentPrice;
        }

        public void SellShares(double saleAmount, double currentPrice)
        {
            var shareAmount = saleAmount / currentPrice;
            _totalShares -= shareAmount;
            Value = _totalShares * currentPrice;
        }
    }
}