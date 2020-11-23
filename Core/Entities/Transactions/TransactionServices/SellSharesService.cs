using System;
using Core.Entities.Users;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface ISellShareService
    {
        UserEntity SellShares(TransactionEntity transaction, bool sellAll);
    }
    
    public class SellSharesService : ISellShareService
    {
        private readonly ICheckExistingHoldingsService _checkExistingHoldingsService;

        public SellSharesService(ICheckExistingHoldingsService checkExistingHoldingsService)
        {
            _checkExistingHoldingsService = checkExistingHoldingsService;
        }

        public UserEntity SellShares(TransactionEntity transaction, bool sellAll)
        {
            var existingHolding = false;

            foreach (var holding in transaction.UserEntity.Holdings)
                if (transaction.Symbol == holding.Symbol)
                    existingHolding = true;

            if (!existingHolding)
                throw new InvalidOperationException("The Holding You Are Trying to Sell Does Not Exist");

            var currentHolding = _checkExistingHoldingsService.CheckExistingHolding(transaction);

            if (sellAll)
                SellAll(currentHolding, transaction);
            else
                SellPartial(currentHolding,transaction);

            currentHolding.SetValue(transaction.CurrentPrice);
            return transaction.UserEntity;
        }

        private void SellPartial(HoldingEntity currentHolding, TransactionEntity transaction)
        {
            var sellShareAmount = transaction.Amount / transaction.CurrentPrice;

            if (sellShareAmount > currentHolding.TotalShares)
                throw new InvalidOperationException(
                    "Cannot sell that many shares, use (sellAll: true) to sell all shares");

            currentHolding.Sell(sellShareAmount);
            transaction.UserEntity.UnallocatedFunds += transaction.Amount;

        }

        private void SellAll(HoldingEntity currentHolding, TransactionEntity transaction)
        {
            var saleValue = currentHolding.SellAll(transaction.CurrentPrice);
            transaction.UserEntity.UnallocatedFunds += saleValue;
            transaction.UserEntity.Holdings.Remove(currentHolding);
        }
    }
}