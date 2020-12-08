using System.IO;
using Core.Entities;
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
            var currentHolding = new Holding(transaction);
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
                currentHolding.User = transaction.User;                
                transaction.User.Holdings.Add(currentHolding);
                transaction.NewHolding = true;
            }

            if (transaction.User.Balance < transaction.Amount)
                throw new StockTransactionException(Path.GetFullPath(ToString()), "PurchaseShares()"); 

            transaction.User.Balance -= transaction.Amount;
            currentHolding.Purchase(transaction.Amount / transaction.TransactionPrice);
            currentHolding.SetValue(transaction.TransactionPrice);
            transaction.Holding = currentHolding; 

            return transaction.User;
        }
    }
}