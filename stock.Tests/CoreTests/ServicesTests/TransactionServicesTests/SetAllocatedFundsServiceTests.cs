using API.Tests.MockClasses;
using Core.Entities;
using Core.Services.IexServices;
using Core.Services.TransactionServices;
using Moq;
using NUnit.Framework;

namespace API.Tests.CoreTests.ServicesTests.TransactionServicesTests
{
    [TestFixture]
    public class SetAllocatedFundsServiceTests
    {
        [Test]
        public void SetAllocatedFunds_WhenCalled_SetsFundsForUser()
        {
            var fakeFetchService = new FakeIexFetchService();
            var user = new User("username", "password", "email@email.com");
            var setAllocatedFundsService = new SetAllocatedFundsService(fakeFetchService);
            
            setAllocatedFundsService.SetAllocatedFunds(user);

            Assert.That(user.AllocatedFunds, Is.EqualTo(2));
        }
    }
}