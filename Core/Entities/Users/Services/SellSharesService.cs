using System;
using Core.Entities.Transactions;
using Core.Models;
using Core.Users.Services;

namespace Core.Entities.Users.Services
{
    public interface ISellShareService
    {
        User SellShares(Transaction transaction);
    }
    
    public class SellSharesService : ISellShareService
    {
        private readonly ICheckExistingHoldingsService _checkExistingHoldingsService;

        public SellSharesService(ICheckExistingHoldingsService checkExistingHoldingsService)
        {
            _checkExistingHoldingsService = checkExistingHoldingsService;
        }

        public User SellShares(Transaction transaction)
        {
            var existingHolding = false;

            foreach (var holding in transaction.User.Holdings)
                if (transaction.Symbol == holding.Symbol)
                    existingHolding = true;

            if (!existingHolding)
                throw new InvalidOperationException("The Holding You Are Trying to Sell Does Not Exist");

            var currentHolding = _checkExistingHoldingsService.CheckExistingHolding(transaction);

            if (transaction.SellAll)
                SellAll(currentHolding, transaction);
            else
                SellPartial(currentHolding,transaction);

            currentHolding.SetValue(transaction.CurrentPrice);
            return transaction.User;
        }

        private void SellPartial(HoldingModel currentHolding, Transaction transaction)
        {
            var sellShareAmount = transaction.Amount / transaction.CurrentPrice;

            if (sellShareAmount > currentHolding.TotalShares)
                throw new InvalidOperationException(
                    "Cannot sell that many shares, use (sellAll: true) to sell all shares");

            currentHolding.Sell(sellShareAmount);
            transaction.User.UnallocatedFunds += transaction.Amount;
        }

        private void SellAll(HoldingModel currentHolding, Transaction transaction)
        {
            var saleValue = currentHolding.SellAll(transaction.CurrentPrice);
            transaction.User.UnallocatedFunds += saleValue;
            transaction.User.Holdings.Remove(currentHolding);
        }
    }
}