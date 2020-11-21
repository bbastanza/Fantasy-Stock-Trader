using System.Collections.Generic;
using Core.Entities.Transactions;
using Core.Models;

namespace Core.Users.Services
{
    public interface IPurchaseSharesService
    {
        HoldingModel PurchaseShares(Transaction transaction, double currentPrice, List<HoldingModel> holdings);
    }
    
    
    public class PurchaseSharesService : IPurchaseSharesService
    {

        public HoldingModel PurchaseShares(Transaction transaction, double currentPrice, List<HoldingModel> holdings)
        {
            HoldingModel currentHolding = new HoldingModel(transaction);
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