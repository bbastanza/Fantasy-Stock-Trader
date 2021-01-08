using Core.Entities;
using Core.Services.IexServices;
using Core.Services.TransactionServices;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using NUnit.Framework;
using Moq;

namespace API.Tests.CoreTests.ServicesTests.TransactionServicesTests
{
    [TestFixture]
    public class HandlePurchaseServiceTests
    {
        private HandlePurchaseService _handlePurchaseService;
        private ISetAllocatedFundsService _setAllocatedFundsService;
        private ICheckExpiration _checkExpiration;
        private IIexFetchService _iexFetchService;

        [SetUp]
        public void SetUp()
        {
            var user = new User("username", "password", "email");

            var checkExpiration = new Mock<ICheckExpiration>();
            checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(user);

            var iexFetchService = new Mock<IIexFetchService>();
            iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(new IexStock() {Symbol = "FAKE", CompanyName = "Fake Stock", LatestPrice = 1});

            var setAllocatedFundsService = new Mock<ISetAllocatedFundsService>();
            setAllocatedFundsService.Setup(x => x.SetAllocatedFunds(user));

            _setAllocatedFundsService = setAllocatedFundsService.Object;
            _iexFetchService = iexFetchService.Object;
            _checkExpiration = checkExpiration.Object;

            _handlePurchaseService =
                new HandlePurchaseService(
                    _iexFetchService,
                    _setAllocatedFundsService,
                    _checkExpiration);
        }

        [Test]
        public void Purchase_WhenNoHoldingExists_ReturnsTransactionWithUserWith1Holding()
        {
            var result = _handlePurchaseService.Purchase("1", 1, "FAKE");

            Assert.That(result.User.Holdings.Count, Is.EqualTo(1));
        }

        [Test]
        public void Purchase_WhenHoldingExists_ReturnsTransactionWithSameNumberOfHoldings()
        {
            // Arrange
            var newUser = new User("username", "password", "email");
            newUser.Holdings.Add(new Holding() {Symbol = "FAKE", CompanyName = "Fake Stock", User = newUser});

            var checkExpiration = new Mock<ICheckExpiration>();
            checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(newUser);

            var handlePurchaseService =
                new HandlePurchaseService(_iexFetchService, _setAllocatedFundsService, checkExpiration.Object);

            // Act
            var result = handlePurchaseService.Purchase("1", 1, "FAKE");

            Assert.That(result.User.Holdings.Count, Is.EqualTo(1));
        }

        [Test]
        public void Purchase_WhenUserComesBackNull_ThrowsNonExistingUserException()
        {
            Assert.That(() => _handlePurchaseService.Purchase("2", 1, "FAKE"),
                Throws.Exception.TypeOf<NonExistingUserException>());
        }

        [Test]
        public void Purchase_WhenCalled_ReturnsExpectedTransaction()
        {
            var result = _handlePurchaseService.Purchase("1", 1, "FAKE");

            Assert.That(result.Type, Is.EqualTo("purchase"));
            Assert.That(result.Amount, Is.EqualTo(1));
            Assert.That(result.SellAll, Is.EqualTo(false));
            Assert.That(result.TransactionPrice, Is.EqualTo(1));
        }

        [Test]
        public void Purchase_WhenCalled_AddsTransactionToUserTransactions()
        {
            var result = _handlePurchaseService.Purchase("1", 1, "FAKE");

            Assert.That(result.User.Transactions.Count, Is.EqualTo(1));
        }

        [Test] // new User.Balance init at 100,000
        public void Purchase_WhenCalled_TransactionUserBalanceMinusAmount()
        {
            var result = _handlePurchaseService.Purchase("1", 99999, "FAKE");

            Assert.That(result.User.Balance, Is.EqualTo(1));
        }
    }
}