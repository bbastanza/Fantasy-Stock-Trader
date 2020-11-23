using System.Collections.Generic;
using Core.Entities.Iex;
using Core.Entities.Users;

namespace Core.Entities.Transactions.TransactionServices
{
    public interface ISetAllocatedFundsService
    {
        double SetAllocatedFunds(List<IexStockModel> stockModels, List<HoldingEntity> holdings);
    }
    
    public class SetAllocatedFundsService : ISetAllocatedFundsService
    {
        public double SetAllocatedFunds(List<IexStockModel> stockModels, List<HoldingEntity> holdings)
        {
            double totalHoldingsValue = 0;
            foreach (var stockModel in stockModels)
            {
                foreach (var holding in holdings)
                    if (stockModel.Symbol == holding.Symbol)
                    {
                        holding.SetValue(stockModel.LatestPrice);
                        totalHoldingsValue += holding.Value;
                    }
            }
            return totalHoldingsValue;
        }
    }
}