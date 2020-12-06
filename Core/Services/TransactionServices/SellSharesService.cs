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
                throw new NonExistingHoldingException(_path, "SellShares()");

            _checkExistingHoldingsService.CheckExistingHolding(transaction);

            if (sellAll)
                SellAll(transaction);
            else
                SellPartial(transaction);

            transaction.Holding.SetValue(transaction.TransactionPrice);
            return transaction.User;
        }

        private void SellPartial(Transaction transaction)
        {
            var sellShareAmount = transaction.Amount / transaction.TransactionPrice;

            if (sellShareAmount > transaction.Holding.TotalShares)
                throw new OverDrawnHoldingException(_path, "SellPartial()");

            transaction.Holding.Sell(sellShareAmount);
            transaction.User.Balance += transaction.Amount;
        }

        private void SellAll(Transaction transaction)
        {
            var saleValue = transaction.Holding.SellAll(transaction.TransactionPrice);
            transaction.User.Balance += saleValue;
            transaction.User.Holdings.Remove(transaction.Holding);
        }
    }
}
