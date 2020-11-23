using System;
using Core.Entities.Users;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface ISellShareService
    {
        UserEntity SellShares(TransactionEntity transactionEntity, bool sellAll);
    }
    
    public class SellSharesService : ISellShareService
    {
        private readonly ICheckExistingHoldingsService _checkExistingHoldingsService;

        public SellSharesService(ICheckExistingHoldingsService checkExistingHoldingsService)
        {
            _checkExistingHoldingsService = checkExistingHoldingsService;
        }

        public UserEntity SellShares(TransactionEntity transactionEntity, bool sellAll)
        {
            var existingHolding = false;

            foreach (var holding in transactionEntity.UserEntity.Holdings)
                if (transactionEntity.Symbol == holding.Symbol)
                    existingHolding = true;

            if (!existingHolding)
                throw new InvalidOperationException("The Holding You Are Trying to Sell Does Not Exist");

            var currentHolding = _checkExistingHoldingsService.CheckExistingHolding(transactionEntity);

            if (sellAll)
                SellAll(currentHolding, transactionEntity);
            else
                SellPartial(currentHolding,transactionEntity);

            currentHolding.SetValue(transactionEntity.CurrentPrice);
            return transactionEntity.UserEntity;
        }

        private void SellPartial(HoldingEntity currentHoldingEntity, TransactionEntity transactionEntity)
        {
            var sellShareAmount = transactionEntity.Amount / transactionEntity.CurrentPrice;

            if (sellShareAmount > currentHoldingEntity.TotalShares)
                throw new InvalidOperationException(
                    "Cannot sell that many shares, use (sellAll: true) to sell all shares");

            currentHoldingEntity.Sell(sellShareAmount);
            transactionEntity.UserEntity.UnallocatedFunds += transactionEntity.Amount;

        }

        private void SellAll(HoldingEntity currentHoldingEntity, TransactionEntity transactionEntity)
        {
            var saleValue = currentHoldingEntity.SellAll(transactionEntity.CurrentPrice);
            transactionEntity.UserEntity.UnallocatedFunds += saleValue;
            transactionEntity.UserEntity.Holdings.Remove(currentHoldingEntity);
        }
    }
}