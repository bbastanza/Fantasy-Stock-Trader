using System.Collections.Generic;
using Core.Models;

namespace Core.Entities.Users.Services
{
    public interface ISetAllocatedFundsService
    {
        double SetAllocatedFunds(List<IexStockModel> stockModels, List<Holding> holdings);
    }
    
    public class SetAllocatedFundsService : ISetAllocatedFundsService
    {
        public double SetAllocatedFunds(List<IexStockModel> stockModels, List<Holding> holdings)
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