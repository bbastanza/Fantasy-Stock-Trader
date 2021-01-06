using System;
using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.IexServices;
using Core.Services.UserServices;
using Infrastructure.Exceptions;

namespace Core.Services.TransactionServices
{
    public interface IHandleSaleService
    {
        Transaction Sell(string sessionId, double shareAmount, string symbol, bool sellAll = false);
    }

    public class HandleSaleService : IHandleSaleService
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly ICheckExpiration _checkExpiration;
        private readonly string _path;

        public HandleSaleService(
            IIexFetchService iexFetchService,
            ISetAllocatedFundsService setAllocatedFundsService,
            ICheckExpiration checkExpiration)
        {
            _iexFetchService = iexFetchService;
            _setAllocatedFundsService = setAllocatedFundsService;
            _checkExpiration = checkExpiration;
            _path = Path.GetFullPath(ToString());
        }

        public Transaction Sell(string sessionId, double shareAmount, string symbol, bool sellAll = false)
        {
            var user = _checkExpiration.CheckUserSession(sessionId);

            if (user == null)
                throw new NonExistingUserException(_path, "Sell()");

            var iexData = _iexFetchService.GetStockBySymbol(symbol);

            var holding = user.Holdings
                .FirstOrDefault(x => x.Symbol == symbol);

            if (holding == null)
                throw new NonExistingHoldingException(_path, "Sell()");
            
            var shareAmountSold = shareAmount;

            if (sellAll)
            {
                shareAmountSold = holding.SellAllReturnShareAmount();
                user.Holdings.Remove(holding);
            }
            
            var transaction = new Transaction()
            {
                Type = "sell",
                Amount = shareAmountSold * iexData.LatestPrice,
                SellAll = sellAll,
                TransactionDate = DateTime.Now,
                TransactionPrice = iexData.LatestPrice,
                User = user,
                Holding = holding
            };
            
            user.Transactions.Add(transaction);
            user.AllocatedFunds = _setAllocatedFundsService.SetAllocatedFunds(user);
            user.Balance += shareAmountSold * iexData.LatestPrice;

            return transaction;
        }
    }
}