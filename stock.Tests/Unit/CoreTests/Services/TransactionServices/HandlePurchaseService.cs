using API.Tests.TestUtilities;
using Core.Entities;
using Core.Services.IexServices;
using Core.Services.TransactionServices;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.Unit.CoreTests.Services.TransactionServices
{
    [TestFixture]
    public class HandlePurchaseServiceTests
    {
        private Mock<ISetAllocatedFundsService> _setAllocatedFundsService;
        private Mock<ICheckExpiration> _checkExpiration;
        private Mock<IIexFetchService> _iexFetchService;
        private User _user;
        private IexStock _stock;

        [SetUp]
        public void SetUp()
        {
            _user = new User(
                TestData.CreateRandomString(), 
                TestData.CreateRandomString(), 
                TestData.CreateRandomString()
                );
            _stock = new IexStock() {Symbol = "FAKE", CompanyName = "Fake Company", LatestPrice = 1};
            _setAllocatedFundsService = new Mock<ISetAllocatedFundsService>();
            _checkExpiration = new Mock<ICheckExpiration>();
            _iexFetchService = new Mock<IIexFetchService>();
        }

        [Test]
        public void Purchase_WhenNoHoldingExists_CreatesASecondHolding()
        {
            _user.Holdings.Add(new Holding());
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var sut =
                new HandlePurchaseService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            sut.Purchase("1", 1, "FAKE");

            Assert.That(_user.Holdings.Count, Is.EqualTo(2));
        }

        [Test]
        public void Purchase_WhenHoldingExists_UserHoldingsCountIsStill1()
        {
            _user.Holdings.Add(new Holding() {Symbol = "FAKE"});
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var sut =
                new HandlePurchaseService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            sut.Purchase("1", 1, "FAKE");

            Assert.That(_user.Holdings.Count, Is.EqualTo(1));
        }

        [Test]
        public void Purchase_WhenUserComesBackNull_ThrowsNonExistingUserException()
        {
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns((User) null);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var sut =
                new HandlePurchaseService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            Assert.That(() => sut.Purchase("1", 1, "FAKE"),
                Throws.Exception.TypeOf<NonExistingUserException>());
        }

        [Test]
        public void Purchase_WhenCalled_AddsCorrectTransactionToUser()
        {
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var sut =
                new HandlePurchaseService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            sut.Purchase("1", 1, "FAKE");

            Assert.That(_user.Transactions[0].Type, Is.EqualTo("purchase"));
            Assert.That(_user.Transactions[0].Amount, Is.EqualTo(1));
            Assert.That(_user.Transactions[0].SellAll, Is.EqualTo(false));
            Assert.That(_user.Transactions[0].TransactionPrice, Is.EqualTo(1));
            Assert.That(_user.Transactions[0].User, Is.EqualTo(_user));
        }

        [Test] // TotalShares of holding should be Amount divided by iexStock.LatestPrice
        public void Purchase_WhenCalled_PurchasesTheCorrectAmount()
        {
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(new IexStock() {Symbol = "FAKE", CompanyName = "Fake Company", LatestPrice = 5});
            var sut =
                new HandlePurchaseService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            sut.Purchase("1", 10, "FAKE");

            Assert.That(_user.Holdings[0].TotalShares, Is.EqualTo(2));
        }

        [Test] // new User.Balance init at 100,000
        public void Purchase_WhenCalled_TransactionUserBalanceMinusAmount()
        {
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var sut =
                new HandlePurchaseService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            sut.Purchase("1", 1, "FAKE");

            Assert.That(_user.Balance, Is.EqualTo(99999));
        }
    }
}