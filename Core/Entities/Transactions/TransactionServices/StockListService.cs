using System.Collections.Generic;
using Core.Entities.Iex;
using Core.Entities.Iex.IexServices;
using Core.Entities.Users;

namespace Core.Entities.Transactions.TransactionServices
{

    public interface IStockListService
    {
        List<IexStockModel> GetStockModelList(UserEntity user);
    }
    
    public class StockListService : IStockListService
    {
        private readonly IIexFetchService _iexFetchService;
        
        public StockListService(IIexFetchService iexFetchService)
        {
            _iexFetchService = iexFetchService;
        }

        public List<IexStockModel> GetStockModelList(UserEntity user)
        {
            var stockModelList = new List<IexStockModel>();
            foreach (var holding in user.Holdings)
            {
                stockModelList.Add(_iexFetchService.GetStockBySymbol(holding.Symbol));
            }
            return stockModelList;
        }
    }
}   
  

