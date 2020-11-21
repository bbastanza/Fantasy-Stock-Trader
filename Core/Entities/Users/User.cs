using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Entities.Transactions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Models
{
    public class User
    {
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        // public User(UserInputModel userInputModel)
        // {
        //     UserName = userModel.UserName;
        //     Password = userModel.Password;
        // }
        
        [JsonPropertyName("userName")]
        public virtual string UserName { get; set; }
        
        [JsonPropertyName("password")]
        public virtual string Password { get; set; }

        [JsonPropertyName("unallocatedDollars")]
        public virtual double UnallocatedFunds { get; set; } = 100000;

        [JsonPropertyName("allocatedDollars")]
        public virtual double AllocatedFunds { get; set; }

        [JsonPropertyName("holdings")]
        public virtual List<HoldingModel> Holdings { get; set; } = new List<HoldingModel>()
            {new HoldingModel(new Transaction() {CompanyName = "Caterpillar", Symbol = "CAT"}) {TotalShares = 30}};

        // made interface    
        public void PurchaseShares(Transaction transaction, double currentPrice)
        {
            UnallocatedFunds -= transaction.Amount;
            var currentHolding = CheckExistingHolding(transaction);
            var purchaseShareAmount = transaction.Amount / currentPrice;
            currentHolding.Purchase(purchaseShareAmount);
            currentHolding.SetValue(currentPrice);
        }

        public void SellShares(Transaction transaction, double currentPrice)
        {
            var existingHolding = false;

            foreach (var holding in Holdings)
                if (transaction.Symbol == holding.Symbol)
                    existingHolding = true;

            if (!existingHolding)
                throw new InvalidOperationException("The Holding You Are Trying to Sell Does Not Exist");

            var currentHolding = CheckExistingHolding(transaction);

            if (transaction.SellAll)
                SellAll(currentHolding, currentPrice);
            else
                SellPartial(currentHolding, currentPrice, transaction);

            currentHolding.SetValue(currentPrice);
        }

        public string ReadHolding(string symbol)
        {
            foreach (var holding in Holdings)
                if (holding.Symbol == symbol)
                    return JsonSerializer.Serialize(holding);

            return "Holding does not exist for this user";
        }

        public void SetAllocatedFunds(List<IexStockModel> iexStockModels)
        {
            double totalHoldingsValue = 0;
            foreach (var stockModel in iexStockModels)
            {
                foreach (var holding in Holdings)
                    if (stockModel.Symbol == holding.Symbol)
                    {
                        holding.SetValue(stockModel.LatestPrice);
                        totalHoldingsValue += holding.Value;
                    }
            }

            AllocatedFunds = totalHoldingsValue;
        }
            
// made interface
        private HoldingModel CheckExistingHolding(Transaction transaction)
        {
            HoldingModel currentHolding = new HoldingModel(transaction);
            var newHolding = true;
            foreach (var holding in Holdings)
                if (transaction.Symbol == holding.Symbol)
                {
                    currentHolding = holding;
                    newHolding = false;
                    break;
                }

            if (newHolding)
                Holdings.Add(currentHolding);

            return currentHolding;
        }

        private void SellPartial(HoldingModel currentHolding, double currentPrice, Transaction transaction )
        {
            var sellShareAmount = transaction.Amount / currentPrice;

            if (sellShareAmount > currentHolding.TotalShares)
                throw new InvalidOperationException(
                    "Cannot sell that many shares, use (sellAll: true) to sell all shares");

            currentHolding.Sell(sellShareAmount);
            UnallocatedFunds += transaction.Amount;
        }

        private void SellAll(HoldingModel currentHolding, double currentPrice)
        {
            var saleValue = currentHolding.SellAll(currentPrice);
            UnallocatedFunds += saleValue;
            Holdings.Remove(currentHolding);
        }
    }
}