using System;
using System.IO;
using Core.Entities;
using Core.Entities.Users;
using Infrastructure.Exceptions;

namespace Core.Services.TransactionServices
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
                throw new StockTransactionException(Path.GetFullPath(ToString()), "SellShares()");

            var currentHolding = _checkExistingHoldingsService.CheckExistingHolding(transaction);

            if (sellAll)
                SellAll(currentHolding, transaction);
            else
                SellPartial(currentHolding, transaction);

            currentHolding.SetValue(transaction.CurrentPrice);
            return transaction.User;
        }

        private void SellPartial(Holding currentHolding, Transaction transaction)
        {
            var sellShareAmount = transaction.Amount / transaction.CurrentPrice;

            if (sellShareAmount > currentHolding.TotalShares)
                throw new StockTransactionException(Path.GetFullPath(ToString()), "SellPartial()");

            currentHolding.Sell(sellShareAmount);
            transaction.User.Balance += transaction.Amount;
        }

        private void SellAll(Holding currentHolding, Transaction transaction)
        {
            var saleValue = currentHolding.SellAll(transaction.CurrentPrice);
            transaction.User.Balance += saleValue;
            transaction.User.Holdings.Remove(currentHolding);
        }
    }
}
