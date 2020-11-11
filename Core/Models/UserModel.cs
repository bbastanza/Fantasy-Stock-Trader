using System.Collections.Generic;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Models
{
    public class UserModel
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; } 
        [JsonPropertyName("password")]
        public string Password { get; set; } 
        [JsonPropertyName("unallocatedDollars")]
        public double UnallocatedDollars { get; set; } = 100000;
        [JsonPropertyName("allocatedDollars")]
        public double AllocatedDollars { get; set; }
        [JsonPropertyName("holdings")]
        public List<HoldingModel> Holdings { get; set; } = new List<HoldingModel>(){new HoldingModel("TSAL")};
        
        public void PurchaseShares(TransactionModel transactionModel,double currentPrice)
        {
            UnallocatedDollars -= transactionModel.Amount;
            var currentHolding = CheckExistingHolding(transactionModel.Symbol);
            var purchaseShareAmount = transactionModel.Amount / currentPrice;
            currentHolding.Purchase(purchaseShareAmount);

        }

        public void SellShares(TransactionModel transactionModel, double currentPrice)
        {
            var currentHolding = CheckExistingHolding(transactionModel.Symbol);
            if (transactionModel.SellAll)
            {
                var saleValue = currentHolding.SellAll(currentPrice);
                UnallocatedDollars += saleValue;
                Holdings.Remove(currentHolding);
            }
            else
            {
                var sellShareAmount = transactionModel.Amount / currentPrice;
                currentHolding.Sell(sellShareAmount);
                UnallocatedDollars += transactionModel.Amount;
            }

        }
        public string ReadHolding(string symbol)
        {
            foreach (var holding in Holdings)
            {
                if (holding.GetSymbol() == symbol)
                {
                    return JsonSerializer.Serialize(holding);
                }
            }
            return JsonSerializer.Serialize(new HoldingModel("null holding"));
        }

        private HoldingModel CheckExistingHolding(string symbol)
        {
            HoldingModel currentHolding = new HoldingModel(symbol);
            while (true)
            {
                foreach (var holding in Holdings)
                {
                    if (symbol == holding.GetSymbol())
                        currentHolding = holding;
                    break;
                }
                Holdings.Add(currentHolding);
                break;
            }
            return currentHolding;
        }

        public void SetAllocatedDollars()
        {
            
        }
    }
}