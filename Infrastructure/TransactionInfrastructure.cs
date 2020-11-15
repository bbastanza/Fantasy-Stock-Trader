using System.Collections.Generic;
using Core.Models;
using Core.Services;

namespace Infrastructure
{

    public interface ITransactionInfrastructure
    {
        List<IexStockModel> GetStockModelList(UserModel userModel);
    }
    
    public class TransactionInfrastructure : ITransactionInfrastructure
    {
        private readonly IIexFetchService _iexFetchService;
        
        public TransactionInfrastructure(IIexFetchService iexFetchService)
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