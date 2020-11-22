using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Transactions;
using Core.Models;

namespace Core.Users.Services
{
    public interface IPurchaseSharesService
    {
        Holding PurchaseShares(Transaction transaction, double currentPrice, List<Holding> holdings);
    }
    
    
    public class PurchaseSharesService : IPurchaseSharesService
    {

        public Holding PurchaseShares(Transaction transaction, double currentPrice, List<Holding> holdings)
        {
            Holding currentHolding = new Holding(transaction);
            var newHolding = true;
            foreach (var holding in holdings)
                if (transaction.Symbol == holding.Symbol)
                {
                    currentHolding = holding;
                    newHolding = false;
                    break;
                }

            if (newHolding)
                holdings.Add(currentHolding);

            return currentHolding;
        }

    }
}