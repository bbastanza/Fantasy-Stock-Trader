using System.Collections.Generic;
using Core.Models;
using Core.Services;

namespace Core.Helpers
{

    public interface ITransactionHelper
    {
        List<IexStockModel> GetStockModelList(UserModel userModel);
    }
    
    public class TransactionHelper : ITransactionHelper
    {
        private readonly IIexFetchService _iexFetchService;
        
        public TransactionHelper(IIexFetchService iexFetchService)
        {
            _iexFetchService = iexFetchService;

        }

        public List<IexStockModel> GetStockModelList(UserModel userModel)
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

