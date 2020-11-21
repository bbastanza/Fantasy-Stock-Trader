using Core.Entities.Transactions;
using Core.Models;

namespace Core.Entities.Users.Services
{
    public interface ICheckExistingHoldingsService
    {
        HoldingModel CheckExistingHolding(Transaction transaction);
    }
    public class CheckExistingHoldingService : ICheckExistingHoldingsService
    {
        public HoldingModel CheckExistingHolding(Transaction transaction)
        {
            HoldingModel currentHolding = new HoldingModel(transaction);
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