using System.Collections.Generic;
using Core.Entities.Iex;
using Core.Entities.Iex.IexServices;
using Core.Entities.Users;

namespace Core.Entities.Transactions.TransactionServices
{

    public interface IStockListService
    {
        List<IexStockModel> GetStockModelList(UserEntity userEntityModel);
    }
    
    public class StockListService : IStockListService
    {
        private readonly IIexFetchService _iexFetchService;
        
        public StockListService(IIexFetchService iexFetchService)
        {
            _iexFetchService = iexFetchService;
        }

        public List<IexStockModel> GetStockModelList(UserEntity userEntityModel)
        {
            var stockModelList = new List<IexStockModel>();
            foreach (var holding in userEntityModel.Holdings)
            {
                stockModelList.Add(_iexFetchService.GetStockBySymbol(holding.Symbol));
            }
            return stockModelList;
        }
    }
}   
  

