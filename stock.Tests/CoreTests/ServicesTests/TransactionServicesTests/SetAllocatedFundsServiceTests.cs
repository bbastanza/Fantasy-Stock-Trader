using API.Tests.MockClasses;
using Core.Entities;
using Core.Services.TransactionServices;
using NUnit.Framework;

namespace API.Tests.CoreTests.ServicesTests.TransactionServicesTests
{
    [TestFixture]
    public class SetAllocatedFundsServiceTests
    {
        [Test] // fakeIexFetchService sets Holding.Value to 1
        public void SetAllocatedFunds_WhenCalled_SetsFundsForUser()
        {
            var fakeHolding = new Holding()
            {
                Symbol = "FAKE",
                CompanyName = "Fake Stock",
                Value = 10,
                TotalShares = 2,
                User = new User()
            };
            var iexFetchService = new FakeIexFetchService();
            var user = new User("username", "password", "email@email.com");
            user.Holdings.Add(fakeHolding);
            user.Holdings.Add(fakeHolding);
            var setAllocatedFundsService = new SetAllocatedFundsService(iexFetchService);

            setAllocatedFundsService.SetAllocatedFunds(user);

            Assert.That(user.AllocatedFunds, Is.EqualTo(2));
        }
    }
}