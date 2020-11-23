using Core.Entities.Users;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface ICheckExistingHoldingsService
    {
        HoldingEntity CheckExistingHolding(TransactionEntity transaction);
    }
    public class CheckExistingHoldingService : ICheckExistingHoldingsService
    {
        public HoldingEntity CheckExistingHolding(TransactionEntity transaction)
        {
            HoldingEntity currentHoldingEntity = new HoldingEntity(transaction);
            var newHolding = true;
            foreach (var holding in transaction.UserEntity.Holdings)
                if (transaction.Symbol == holding.Symbol)
                {
                    currentHoldingEntity = holding;
                    newHolding = false;
                    break;
                }

            if (newHolding)
                transaction.UserEntity.Holdings.Add(currentHoldingEntity);

            return currentHoldingEntity;
        } 
    }
}