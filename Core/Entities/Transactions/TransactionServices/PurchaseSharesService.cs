using System.Collections.Generic;
using Core.Entities.Users;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface IPurchaseSharesService
    {
        UserEntity PurchaseShares(TransactionEntity transactionEntity);
    }
    
    public class PurchaseSharesService : IPurchaseSharesService
    {

        public UserEntity PurchaseShares(TransactionEntity transactionEntity)
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
            
            transactionEntity.UserEntity.UnallocatedFunds -= transactionEntity.Amount;
            currentHoldingEntity.Purchase(transactionEntity.Amount / transactionEntity.CurrentPrice);
            currentHoldingEntity.SetValue(transactionEntity.CurrentPrice);

            return transactionEntity.UserEntity;
        }

    }
}