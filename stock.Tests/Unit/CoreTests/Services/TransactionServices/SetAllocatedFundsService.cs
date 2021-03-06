using API.Tests.TestUtilities;
using Core.Entities;
using Core.Services.IexServices;
using Core.Services.TransactionServices;
using Moq;
using NUnit.Framework;

namespace API.Tests.Unit.CoreTests.Services.TransactionServices
{
    [TestFixture]
    public class SetAllocatedFundsServiceTests
    {
        [Test] 
        public void SetAllocatedFunds_WhenCalled_SetsFundsForUser()
        {
            var holding = TestData.MockHolding;
            var iexFetchService = new Mock<IIexFetchService>();
            iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(new IexStock() {Symbol = "Fake", CompanyName = "Fake Stock", LatestPrice = 1});
            var user = new User("username", "password", "email@email.com");
            user.Holdings.Add(holding);
            var sut = new SetAllocatedFundsService(iexFetchService.Object);

            sut.SetAllocatedFunds(user);

            Assert.That(user.AllocatedFunds, Is.EqualTo(2));
        }
    }
}