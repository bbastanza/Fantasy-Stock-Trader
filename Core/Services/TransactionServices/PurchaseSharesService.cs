using System.IO;
using Core.Entities;
using Core.Entities.Users;
using Infrastructure.Exceptions;

namespace Core.Services.TransactionServices
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
            {
                // currentHolding.User = transaction.User;                
                transaction.User.Holdings.Add(currentHolding);
            }

            if (transaction.User.Balance < transaction.Amount)
                throw new StockTransactionException(Path.GetFullPath(ToString()), "PurchaseShares()"); 

            transaction.User.Balance -= transaction.Amount;
            currentHolding.Purchase(transaction.Amount / transaction.CurrentPrice);
            currentHolding.SetValue(transaction.CurrentPrice);

            return transaction.User;
        }
    }
}