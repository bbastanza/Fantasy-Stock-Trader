using System.Collections.Generic;
using Core.Models;
using Core.Services;

namespace Infrastructure
{
    public class TransactionInfrastructure
    {
        private readonly UserModel _userModel;
        private readonly IIexFetchService _iexFetchService;

        public TransactionInfrastructure(UserModel userModel,IIexFetchService iexFetchService)
        {
            _userModel = userModel;
            _iexFetchService = iexFetchService;
            var stockModelList = new List<IexStockModel>();
            foreach (var holding in _userModel.Holdings)
            {
                stockModelList.Add(_iexFetchService.GetStockBySymbol(holding.Symbol));
            }
            StockModelList = stockModelList;
        }

        public List<IexStockModel> StockModelList { get; }
    }
}