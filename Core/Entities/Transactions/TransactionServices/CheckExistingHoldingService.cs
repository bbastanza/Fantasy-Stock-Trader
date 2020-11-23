using Core.Entities.Users;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface ICheckExistingHoldingsService
    {
        HoldingEntity CheckExistingHolding(TransactionEntity transactionEntity);
    }
    public class CheckExistingHoldingService : ICheckExistingHoldingsService
    {
        public HoldingEntity CheckExistingHolding(TransactionEntity transactionEntity)
        {
            HoldingEntity currentHoldingEntity = new HoldingEntity(transactionEntity);
            var newHolding = true;
            foreach (var holding in transactionEntity.UserEntity.Holdings)
                if (transactionEntity.Symbol == holding.Symbol)
                {
                    currentHoldingEntity = holding;
                    newHolding = false;
                    break;
                }

            if (newHolding)
                transactionEntity.UserEntity.Holdings.Add(currentHoldingEntity);

            return currentHoldingEntity;
        } 
    }
}