using System.IO;
using Core.Entities;
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
        private readonly string _path;

        public SellSharesService(ICheckExistingHoldingsService checkExistingHoldingsService)
        {
            _checkExistingHoldingsService = checkExistingHoldingsService;
            _path = Path.GetFullPath(ToString());
        }

        public User SellShares(Transaction transaction, bool sellAll)
        {
            var existingHolding = transaction.User.Holdings
                .Exists(x => x.Symbol == transaction.Symbol);

            if (!existingHolding)
                throw new StockTransactionException(_path, "SellShares()");

            _checkExistingHoldingsService.CheckExistingHolding(transaction);

            if (sellAll)
                SellAll(transaction.Holding, transaction);
            else
                SellPartial(transaction.Holding, transaction);

            transaction.Holding.SetValue(transaction.TransactionPrice);
            return transaction.User;
        }

        private void SellPartial(Holding currentHolding, Transaction transaction)
        {
            var sellShareAmount = transaction.Amount / transaction.TransactionPrice;

            if (sellShareAmount > currentHolding.TotalShares)
                throw new StockTransactionException(Path.GetFullPath(ToString()), "SellPartial()");

            currentHolding.Sell(sellShareAmount);
            transaction.User.Balance += transaction.Amount;
        }

        private void SellAll(Holding currentHolding, Transaction transaction)
        {
            var saleValue = currentHolding.SellAll(transaction.TransactionPrice);
            transaction.User.Balance += saleValue;
            transaction.User.Holdings.Remove(currentHolding);
        }
    }
}
