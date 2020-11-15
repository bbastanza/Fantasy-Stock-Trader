using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Models
{
    public class UserModel
    {
        public UserModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public UserModel(UserModel userModel)
        {
            UserName = userModel.UserName;
            Password = userModel.Password;
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
        public List<HoldingModel> Holdings { get; set; } = new List<HoldingModel>(){new HoldingModel(new TransactionModel(){CompanyName = "Caterpillar",Symbol = "CAT"}){TotalShares = 30}};
        
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
            var existingHolding = false;
            
            foreach (var holding in Holdings)
                if (transactionModel.Symbol == holding.Symbol)
                    existingHolding = true;
            
            if (!existingHolding)
                throw new ApplicationException("The Holding You Are Trying to Sell Does Not Exist");
            
            var currentHolding = CheckExistingHolding(transactionModel);
            
            if (transactionModel.SellAll)
               SellAll(currentHolding, currentPrice);
            else
                SellPartial(currentHolding, transactionModel, currentPrice);

            currentHolding.SetValue(currentPrice);
        }

        public string ReadHolding(string symbol)
        {
            foreach (var holding in Holdings)
            {
                if (holding.Symbol == symbol)
                    return JsonSerializer.Serialize(holding);
            }
            
            return "Holding does not exist for this user";
        }

        public void SetAllocatedDollars(List<IexStockModel> iexStockModels)
        {
            double totalHoldingsValue = 0;
            foreach (var stockModel in iexStockModels)
            {
                foreach (var holding in Holdings)
                {
                    if (stockModel.Symbol == holding.Symbol)
                    {
                        holding.SetValue(stockModel.LatestPrice);
                        totalHoldingsValue += holding.Value;
                    }
                }
            }
            AllocatedDollars = totalHoldingsValue;
        }
        
        private HoldingModel CheckExistingHolding(TransactionModel transactionModel)
        {
            HoldingModel currentHolding = new HoldingModel(transactionModel);
            var newHolding = true;
            foreach (var holding in Holdings)
            {
                if (transactionModel.Symbol == holding.Symbol)
                {
                    currentHolding = holding;
                    newHolding = false;
                    break;
                }
            }
            
            if (newHolding)
                Holdings.Add(currentHolding);
            
            return currentHolding;
        }

        private void SellPartial(HoldingModel currentHolding,TransactionModel transactionModel ,double currentPrice)
        {
                var sellShareAmount = transactionModel.Amount / currentPrice;
                currentHolding.Sell(sellShareAmount);
                UnallocatedDollars += transactionModel.Amount;
        }

        private void SellAll(HoldingModel currentHolding, double currentPrice)
        {
                var saleValue = currentHolding.SellAll(currentPrice);
                UnallocatedDollars += saleValue;
                Holdings.Remove(currentHolding);
        }
    }
}