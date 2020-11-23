using System.Collections.Generic;
using Core.Entities.Users;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface IPurchaseSharesService
    {
        UserEntity PurchaseShares(TransactionEntity transaction);
    }
    
    public class PurchaseSharesService : IPurchaseSharesService
    {

        public UserEntity PurchaseShares(TransactionEntity transaction)
        {
            HoldingEntity currentHolding = new HoldingEntity(transaction);
            var newHolding = true;
            foreach (var holding in transaction.UserEntity.Holdings)
                if (transaction.Symbol == holding.Symbol)
                {
                    currentHolding = holding;
                    newHolding = false;
                    break;
                }

            if (newHolding)
                transaction.UserEntity.Holdings.Add(currentHolding);
            
            transaction.UserEntity.UnallocatedFunds -= transaction.Amount;
            currentHolding.Purchase(transaction.Amount / transaction.CurrentPrice);
            currentHolding.SetValue(transaction.CurrentPrice);

            return transaction.UserEntity;
        }

    }
}