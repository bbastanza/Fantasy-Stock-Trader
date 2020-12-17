using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services.IexServices;

namespace Core.Services.TransactionServices
{
    public interface ISetAllocatedFundsService
    {
        double SetAllocatedFunds(User user);
    }

    public class SetAllocatedFundsService : ISetAllocatedFundsService
    {
        private readonly IIexFetchService _iexFetchService;

        public SetAllocatedFundsService(IIexFetchService iexFetchService)
        {
            _iexFetchService = iexFetchService;
        }

        public double SetAllocatedFunds(User user)
        {
            double totalHoldingsValue = 0;

            foreach (var holding in user.Holdings)
            {
                _iexFetchService.UpdateHolding(holding);
                totalHoldingsValue += holding.Value;
            }

            return totalHoldingsValue;
        }
    }
}