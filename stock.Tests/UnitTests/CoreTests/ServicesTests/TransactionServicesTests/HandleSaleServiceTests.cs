using Core.Entities;
using Core.Services.IexServices;
using Core.Services.TransactionServices;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.UnitTests.CoreTests.ServicesTests.TransactionServicesTests
{
    [TestFixture]
    public class HandleSaleServiceTests
    {
        private User _user;
        private IexStock _stock;
        private Mock<ISetAllocatedFundsService> _setAllocatedFundsService;
        private Mock<ICheckExpiration> _checkExpiration;
        private Mock<IIexFetchService> _iexFetchService;
        private Holding _holding;

        [SetUp]
        public void SetUp()
        {
            _user = new User("username", "password", "email");
            _stock = new IexStock() {Symbol = "FAKE", CompanyName = "Fake Company", LatestPrice = 1};
            _holding = new Holding() {Symbol = "FAKE", CompanyName = "Fake Company", TotalShares = 10, User = _user};
            _setAllocatedFundsService = new Mock<ISetAllocatedFundsService>();
            _checkExpiration = new Mock<ICheckExpiration>();
            _iexFetchService = new Mock<IIexFetchService>();
        }

        [Test]
        public void Sell_WhenUserComesBackNull_ThrowsNonExistingUserException()
        {
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns((User) null);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var handleSaleService =
                new HandleSaleService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);

            Assert.That(() => handleSaleService.Sell("1", 1, "FAKE"),
                Throws.Exception.TypeOf<NonExistingUserException>());
        }

        [Test]
        public void Sell_WhenHoldingComesBackNull_ThrowsNonExistingHoldingException()
        {
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var handleSaleService =
                new HandleSaleService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            Assert.That(() => handleSaleService.Sell("1", 1, "FAKE"),
                Throws.Exception.TypeOf<NonExistingHoldingException>());
        }

        [Test]
        public void Sell_WhenSellAllIsTrue_HoldingIsRemovedFromUser()
        {
            _user.Holdings.Add(_holding);
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var handleSaleService =
                new HandleSaleService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            handleSaleService.Sell("1", 1, "FAKE", true);

            Assert.That(_user.Holdings.Count, Is.EqualTo(0));
        }

        [Test] // User.Balance is 100000 => _holding.TotalShares is 10
        public void Sell_WhenSellAllIsTrue_DollarAmountIsAddedToUserBalance()
        {
            _user.Holdings.Add(_holding);
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var handleSaleService =
                new HandleSaleService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            handleSaleService.Sell("1", 1, "FAKE", true);

            Assert.That(_user.Balance, Is.EqualTo(100010));
        }
        
        [Test] // _holding.TotalShares is 10
        public void Sell_WhenSellAllIsFalse_ShareAmountIsReducedFromHoldingTotalShares()
        {
            _user.Holdings.Add(_holding);
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var handleSaleService =
                new HandleSaleService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            handleSaleService.Sell("1", 1, "FAKE");

            Assert.That(_user.Holdings[0].TotalShares, Is.EqualTo(9));
        }
        
        [Test] // User.Balance is 100000 
        public void Sell_WhenSellAllIsFalse_DollarAmountIsAddedToUserBalance()
        {
            _user.Holdings.Add(_holding);
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var handleSaleService =
                new HandleSaleService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            handleSaleService.Sell("1", 1, "FAKE" );

            Assert.That(_user.Balance, Is.EqualTo(100001));
        }
        
        [Test] 
        public void Sell_WhenCalled_CorrectTransactionIsAddedToUserTransactions()
        {
            _user.Holdings.Add(_holding);
            _checkExpiration.Setup(x => x.CheckUserSession("1"))
                .Returns(_user);
            _iexFetchService.Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(_stock);
            var handleSaleService =
                new HandleSaleService(_iexFetchService.Object, _setAllocatedFundsService.Object,
                    _checkExpiration.Object);
            
            handleSaleService.Sell("1", 1, "FAKE" );

            Assert.That(_user.Transactions[0].Type, Is.EqualTo("sell"));
            Assert.That(_user.Transactions[0].Amount, Is.EqualTo(1));
            Assert.That(_user.Transactions[0].SellAll, Is.EqualTo(false));
            Assert.That(_user.Transactions[0].TransactionPrice, Is.EqualTo(1));
            Assert.That(_user.Transactions[0].User, Is.EqualTo(_user));
            Assert.That(_user.Transactions[0].Holding, Is.EqualTo(_holding));
        }
    }
}