using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Users;
using Core.Services.IexServices;

namespace Core.Services.TransactionServices
{

    public interface IStockListService
    {
        List<IexStock> GetStockModelList(User user);
    }
    
    public class StockListService : IStockListService
    {
        private readonly IIexFetchService _iexFetchService;
        
        public StockListService(IIexFetchService iexFetchService)
        {
            _iexFetchService = iexFetchService;
        }

        public List<IexStock> GetStockModelList(User user)
        {
            var stockModelList = new List<IexStock>();
            foreach (var holding in user.Holdings)
            {
                stockModelList.Add(_iexFetchService.GetStockBySymbol(holding.Symbol));
            }
            return stockModelList;
        }
    }
}   
  

