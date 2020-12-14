using System;
using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using NHibernate;
using NHibernate.Linq;

namespace Core.Services.TransactionServices
{
    public interface IHandleSaleService
    {
        Transaction Sell(double amount, string userName, string symbol, bool sellAll = false);
    }

    public class HandleSaleService : IHandleSaleService
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly string _path;
        private readonly ISession _session;

        public HandleSaleService(
            IIexFetchService iexFetchService,
            ISetAllocatedFundsService setAllocatedFundsService,
            INHibernateSession inHibernateSession)
        {
            _iexFetchService = iexFetchService;
            _setAllocatedFundsService = setAllocatedFundsService;
            _session = inHibernateSession.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public Transaction Sell(double amount, string userName, string symbol, bool sellAll = false)
        {
            var user = _session.Query<User>().FirstOrDefault(x => x.UserName == userName);

            if (user == null)
                throw new NonExistingUserException(_path, "Sell()");

            var iexData = _iexFetchService.GetStockBySymbol(symbol);

            var holding = user.Holdings
                .FirstOrDefault(x => x.Symbol == symbol);

            if (holding == null)
                throw new NonExistingHoldingException(_path, "Sell()");
            
            var amountSold = amount;

            if (sellAll)
            {
                amountSold = holding.SellAll(iexData.LatestPrice);
                user.Holdings.Remove(holding);
            }
            else
            {
                var overdrawn = holding.Sell(amount / iexData.LatestPrice);
                if (overdrawn)
                    throw new OverDrawnHoldingException(_path, "Sell()");
            }
            
            var transaction = new Transaction()
            {
                Type = "sell",
                Amount = amountSold,
                SellAll = sellAll,
                TransactionDate = DateTime.Now,
                TransactionPrice = iexData.LatestPrice,
                User = user,
                Holding = holding
            };
            
            user.Transactions.Add(transaction);
            user.AllocatedFunds = _setAllocatedFundsService.SetAllocatedFunds(user);
            user.Balance += amountSold;

            return transaction;
        }
    }
}