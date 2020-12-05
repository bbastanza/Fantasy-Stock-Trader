using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Core.Services.TransactionServices
{
    public interface ISetAllocatedFundsService
    {
        double SetAllocatedFunds(List<IexStock> stockModels, List<Holding> holdings);
    }
    
    public class SetAllocatedFundsService : ISetAllocatedFundsService
    {
        public double SetAllocatedFunds(List<IexStock> stockModels, List<Holding> holdings)
        {
            double totalHoldingsValue = 0;
            
            foreach (var stockModel in stockModels)
                foreach (var holding in holdings
                    .Where(holding => stockModel.Symbol == holding.Symbol))
                {
                    holding.SetValue(stockModel.LatestPrice);
                    totalHoldingsValue += holding.Value;
                }
            
            return totalHoldingsValue;
        }
    }
}