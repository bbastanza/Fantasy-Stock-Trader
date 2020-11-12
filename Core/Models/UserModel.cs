using System.Collections.Generic;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Models
{
    public class UserModel
    {

        // public UserModel(UserModel userModel)
        // ^ this may work, but not yet
        public UserModel(string userName, string password)
        {
            // UserName = userModel.UserName;
            // Password = userModel.Password;
            UserName = userName;
            Password = password;
        }
        
        [JsonPropertyName("userName")]
        public string UserName { get; set; } 
        [JsonPropertyName("password")]
        public string Password { get; set; } 
        [JsonPropertyName("unallocatedDollars")]
        public double UnallocatedDollars { get; set; } = 100000;
        [JsonPropertyName("allocatedDollars")]
        public double AllocatedDollars { get; set; }
        [JsonPropertyName("holdings")]
        public List<HoldingModel> Holdings { get; set; } = new List<HoldingModel>();
        
        public void PurchaseShares(TransactionModel transactionModel,double currentPrice)
        {
            UnallocatedDollars -= transactionModel.Amount;
            var currentHolding = CheckExistingHolding(transactionModel);
            var purchaseShareAmount = transactionModel.Amount / currentPrice;
            currentHolding.Purchase(purchaseShareAmount);
            currentHolding.SetValue(currentPrice);
        }

        public void SellShares(TransactionModel transactionModel, double currentPrice)
        {
            var currentHolding = CheckExistingHolding(transactionModel);
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
            currentHolding.SetValue(currentPrice);
        }
        public string ReadHolding(string symbol)
        {
            foreach (var holding in Holdings)
            {
                if (holding.Symbol == symbol)
                {
                    return JsonSerializer.Serialize(holding);
                }
            }
            return "Holding does not exist for this user";
        }

        private HoldingModel CheckExistingHolding(TransactionModel transactionModel)
        {
            HoldingModel currentHolding = new HoldingModel(transactionModel);
            while (true)
            {
                foreach (var holding in Holdings)
                {
                    if (transactionModel.Symbol == holding.Symbol)
                        currentHolding = holding;
                    break;
                }
                Holdings.Add(currentHolding);
                break;
            }
            return currentHolding;
        }

        public void SetAllocatedDollars(List<string> symbols)
        {
            double totalHoldingsValue = 0;
            foreach (var symbol in symbols)
            {
                foreach (var holding in Holdings)
                {
                    if (symbol == holding.Symbol)
                    {
                        totalHoldingsValue += holding.Value;
                    }
                }
            }

            AllocatedDollars = totalHoldingsValue;
        }
        
 
    }
}