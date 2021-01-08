using Core.Entities;
using Core.Services.IexServices;

namespace API.Tests.MockClasses
{
    public class FakeIexFetchService : IIexFetchService
    {
        public IexStock GetStockBySymbol(string stockName)
        {
            return new IexStock() {Symbol = "FAKE", CompanyName = "Fake Stock", LatestPrice = 1};
        }

        public void UpdateHolding(Holding holding)
        {
            holding.Value = 1;
        }
    }
}