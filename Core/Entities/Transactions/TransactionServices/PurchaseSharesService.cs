using System.Collections.Generic;
using Core.Entities.Users;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface IPurchaseSharesService
    {
        User PurchaseShares(Transaction transaction);
    }
    
    public class PurchaseSharesService : IPurchaseSharesService
    {

        public User PurchaseShares(Transaction transaction)
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
            
            transaction.User.UnallocatedFunds -= transaction.Amount;
            currentHolding.Purchase(transaction.Amount / transaction.CurrentPrice);
            currentHolding.SetValue(transaction.CurrentPrice);

            return transaction.User;
        }

    }
}