using API.Controllers;
using Core.Entities;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.APITests.ControllerTests
{
    [TestFixture]
    public class StockDataControllerTests
    {
        private Mock<IIexFetchService> _iexFetchService;
        private StockDataController _stockDataController;

        [SetUp]
        public void SetUp()
        {
            _iexFetchService = new Mock<IIexFetchService>();
            _iexFetchService
                .Setup(x => x.GetStockBySymbol("FAKE"))
                .Returns(new IexStock());

            _stockDataController = new StockDataController(_iexFetchService.Object);
        }
        
        [Test]
        public void GetStockData_NullStockSymbol_ThrowsInvalidSymbolException()
        {
            Assert.That(() => _stockDataController.GetStockData(null),
                Throws.Exception.TypeOf<InvalidSymbolException>());
        }
    }
}