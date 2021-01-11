using Core.Entities;
using Core.Services.TransactionServices;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.CoreTests.ServicesTests.UserServicesTests
{
    [TestFixture]
    public class GetUserDataServiceTests
    {
        private Mock<ISetAllocatedFundsService> _setAllocatedFundsService;
        private Mock<ICheckExpiration> _checkExpiration;
        private User _user;

        [SetUp]
        public void SetUp()
        {
            _setAllocatedFundsService = new Mock<ISetAllocatedFundsService>();
            _checkExpiration = new Mock<ICheckExpiration>();
            _user = new User("username", "password", "email");
        }

        [Test]
        public void GetUserData_NullUser_ThrowsNonExistingUserException()
        {
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns((User) null);
            var getUserDataService = new GetUserDataService(_setAllocatedFundsService.Object, _checkExpiration.Object);

            Assert.That(() => getUserDataService.GetUserData("1"), Throws.Exception.TypeOf<NonExistingUserException>());
        }
        
        [Test]
        public void GetUserTransactions_ValidUser_ReturnsUserTransactions()
        {
            _user.Transactions.Add(new Transaction());
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            var getUserDataService = new GetUserDataService(_setAllocatedFundsService.Object, _checkExpiration.Object);

            var result = getUserDataService.GetUserTransactions("1");

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result, Is.EqualTo(_user.Transactions));
        }
        
        [Test]
        public void GetUserData_ValidUser_ReturnsUser()
        {
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            var getUserDataService = new GetUserDataService(_setAllocatedFundsService.Object, _checkExpiration.Object);

            var result = getUserDataService.GetUserData("1");
            
            Assert.That(result, Is.EqualTo(_user));
        }
    }
}