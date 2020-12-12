using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services.IexServices;

namespace Core.Services.TransactionServices
{
    public interface ISetAllocatedFundsService
    {
        double SetAllocatedFunds(User user);
    }
    
    public class SetAllocatedFundsService : ISetAllocatedFundsService
    {
        private readonly IIexFetchService _iexFetchService;

        public SetAllocatedFundsService(IIexFetchService iexFetchService)
        {
            _iexFetchService = iexFetchService;
        }
        public double SetAllocatedFunds(User user)
        {
            var stockModels = GetStockModelList(user);
            
            double totalHoldingsValue = 0;
            
            foreach (var stockModel in stockModels)
                foreach (var holding in user.Holdings
                    .Where(holding => stockModel.Symbol == holding.Symbol))
                {
                    holding.SetValue(stockModel.LatestPrice);
                    totalHoldingsValue += holding.Value;
                }
            
            return totalHoldingsValue;
        }
        
        private List<IexStock> GetStockModelList(User user)
        {
            var stockModelList = new List<IexStock>();
            
            foreach (var holding in user.Holdings)
                stockModelList.Add(_iexFetchService.GetStockBySymbol(holding.Symbol));
            
            return stockModelList;
        }
    }
}