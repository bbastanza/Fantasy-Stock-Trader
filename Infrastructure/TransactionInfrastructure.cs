using System.Collections.Generic;
using Core.Models;

namespace Infrastructure
{
    public class TransactionInfrastructure
    {
        private readonly UserModel _userModel;

        public TransactionInfrastructure(UserModel userModel)
        {
            _userModel = userModel;
        }
        public List<string> GetUserSymbols()
        {
            var symbolList = new List<string>();
            foreach (var holding in _userModel.Holdings)
            {
                symbolList.Add(holding.Symbol);
            }
            return symbolList;
        }
    }
}