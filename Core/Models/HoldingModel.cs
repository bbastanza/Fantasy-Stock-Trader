using System.Text.Json.Serialization;

namespace Core.Models
{
    public class HoldingModel : StockModel
    {
        private double _totalShares;

        public HoldingModel()
        {
            _totalShares += PurchaseShares;
        }
        
        [JsonPropertyName("purchaseAmount")] 
        public double PurchaseAmount { get; set; }

        [JsonPropertyName("purchaseShareAmount")]
        public double PurchaseShares => PurchaseAmount / IexRealtimePrice;

        [JsonPropertyName("value")] 
        public double Value => _totalShares * IexRealtimePrice;

        public double TotalShares
        {
            get => _totalShares;
            set => _totalShares = value;
        }

        // public void AddPurchaseToTotal()
        // {
        //     _totalShares += PurchaseShares;
        // }
    }
}