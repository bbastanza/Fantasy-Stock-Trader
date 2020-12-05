using System.Linq;
using Core.Entities;

namespace Core.Services.TransactionServices
{
    public interface ICheckExistingHoldingsService
    {
        void CheckExistingHolding(Transaction transaction);
    }

    public class CheckExistingHoldingService : ICheckExistingHoldingsService
    {
        public void CheckExistingHolding(Transaction transaction)
        {
            transaction.Holding = new Holding(transaction);

            var newHolding = true;

            foreach (var holding in transaction.User.Holdings
                .Where(holding => holding.Symbol == transaction.Symbol))
            {
                transaction.Holding = holding;
                newHolding = false;
                break;
            }

            if (newHolding)
                transaction.User.Holdings.Add(transaction.Holding);
        }
    }
}