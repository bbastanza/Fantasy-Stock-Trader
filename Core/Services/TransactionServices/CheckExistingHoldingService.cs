using Core.Entities;

namespace Core.Services.TransactionServices
{
    public interface ICheckExistingHoldingsService
    {
        Holding CheckExistingHolding(Transaction transaction);
    }
    public class CheckExistingHoldingService : ICheckExistingHoldingsService
    {
        public Holding CheckExistingHolding(Transaction transaction)
        {
            Holding currentHolding = new Holding(transaction);
            var newHolding = true;
            foreach (var holding in transaction.User.Holdings)
                if (transaction.Symbol == holding.Symbol)
                {
                    currentHolding = holding;
                    newHolding = false;
                    break;
                }

            if (newHolding)
                transaction.User.Holdings.Add(currentHolding);

            return currentHolding;
        } 
    }
}