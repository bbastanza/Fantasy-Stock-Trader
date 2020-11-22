using System.Collections.Generic;
using Core.Entities.Users;
using Core.Models;
using Core.Services;

namespace Core.Entities.Transactions.TransactionServices
{

    public interface IStockListService
    {
        List<IexStockModel> GetStockModelList(User userModel);
    }
    
    public class StockListService : IStockListService
    {
        private readonly IIexFetchService _iexFetchService;
        
        public StockListService(IIexFetchService iexFetchService)
        {
            _iexFetchService = iexFetchService;
        }

        public List<IexStockModel> GetStockModelList(User userModel)
        {
            var stockModelList = new List<IexStockModel>();
            foreach (var holding in userModel.Holdings)
            {
                stockModelList.Add(_iexFetchService.GetStockBySymbol(holding.Symbol));
            }
            return stockModelList;
        }
    }
}   
    // User ID=postgres;Password=password;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;}

