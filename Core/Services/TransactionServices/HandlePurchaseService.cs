using System;
using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.IexServices;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.TransactionServices
{
    public interface IHandlePurchaseService
    {
        Transaction Purchase(string sessionId, double amount, string symbol);
    }

    public class HandlePurchaseService : IHandlePurchaseService
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly ICheckExpiration _checkExpiration;
        private readonly string _path;

        public HandlePurchaseService(
            IIexFetchService iexFetchService,
            ISetAllocatedFundsService setAllocatedFundsService,
            ICheckExpiration checkExpiration)
        {
            _iexFetchService = iexFetchService;
            _setAllocatedFundsService = setAllocatedFundsService;
            _checkExpiration = checkExpiration;
            _path = Path.GetFullPath(ToString());
        }


        public Transaction Purchase(string sessionId, double amount, string symbol)
        {
            Console.WriteLine(sessionId);
            var user = _checkExpiration.CheckUserSession(sessionId);

            if (user == null)
                throw new NonExistingUserException(_path, "PurchaseTransaction()");

            var iexData = _iexFetchService.GetStockBySymbol(symbol);

            var holding = user.Holdings
                .FirstOrDefault(x => x.Symbol == symbol);

            if (holding == null)
            {
                holding = new Holding()
                {
                    Symbol = iexData.Symbol,
                    CompanyName = iexData.CompanyName,
                    User = user,
                };
                user.Holdings.Add(holding);
            }

            holding.Purchase(amount / iexData.LatestPrice);

            var transaction = new Transaction()
            {
                Type = "purchase",
                Amount = amount,
                SellAll = false,
                TransactionDate = DateTime.Now,
                TransactionPrice = iexData.LatestPrice,
                User = user,
                Holding = holding
            };
            
            user.Transactions.Add(transaction);
            user.AllocatedFunds = _setAllocatedFundsService.SetAllocatedFunds(user);
            user.Balance -= amount;

            return transaction;
        }
    }
}