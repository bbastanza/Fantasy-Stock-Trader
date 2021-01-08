using Core.Entities;
using Core.Services.IexServices;

namespace Core.Services.TransactionServices
{
    public interface ISetAllocatedFundsService
    {
        void SetAllocatedFunds(User user);
    }

    public class SetAllocatedFundsService : ISetAllocatedFundsService
    {
        private readonly IIexFetchService _iexFetchService;

        public SetAllocatedFundsService(IIexFetchService iexFetchService)
        {
            _iexFetchService = iexFetchService;
        }

        public void SetAllocatedFunds(User user)
        {
            double totalHoldingsValue = 0;

            foreach (var holding in user.Holdings)
            {
                _iexFetchService.UpdateHolding(holding);
                totalHoldingsValue += holding.Value;
            }

            user.AllocatedFunds = totalHoldingsValue;
        }
    }
}