using System;
using Core.Entities.Users;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface ISellShareService
    {
        User SellShares(Transaction transaction, bool sellAll);
    }
    
    public class SellSharesService : ISellShareService
    {
        private readonly ICheckExistingHoldingsService _checkExistingHoldingsService;

        public SellSharesService(ICheckExistingHoldingsService checkExistingHoldingsService)
        {
            _checkExistingHoldingsService = checkExistingHoldingsService;
        }

        public User SellShares(Transaction transaction, bool sellAll)
        {
            var existingHolding = false;

            foreach (var holding in transaction.User.Holdings)
                if (transaction.Symbol == holding.Symbol)
                    existingHolding = true;

            if (!existingHolding)
                throw new InvalidOperationException("The Holding You Are Trying to Sell Does Not Exist");

            var currentHolding = _checkExistingHoldingsService.CheckExistingHolding(transaction);

            if (sellAll)
                SellAll(currentHolding, transaction);
            else
                SellPartial(currentHolding,transaction);

            currentHolding.SetValue(transaction.PurchasePrice);
            return transaction.User;
        }

        private void SellPartial(Holding currentHolding, Transaction transaction)
        {
            var sellShareAmount = transaction.Amount / transaction.PurchasePrice;

            if (sellShareAmount > currentHolding.TotalShares)
                throw new InvalidOperationException(
                    "Cannot sell that many shares, use (sellAll: true) to sell all shares");

            currentHolding.Sell(sellShareAmount);
            transaction.User.UnallocatedFunds += transaction.Amount;

        }

        private void SellAll(Holding currentHolding, Transaction transaction)
        {
            var saleValue = currentHolding.SellAll(transaction.PurchasePrice);
            transaction.User.UnallocatedFunds += saleValue;
            transaction.User.Holdings.Remove(currentHolding);
        }
    }
}