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
        private StockDataController _stockDataController;

        [SetUp]
        public void SetUp()
        {
            var iexFetchService = new Mock<IIexFetchService>();
            _stockDataController = new StockDataController(iexFetchService.Object);
        }
        
        [Test]
        public void GetStockData_NullStockSymbol_ThrowsInvalidSymbolException()
        {
            Assert.That(() => _stockDataController.GetStockData(null),
                Throws.Exception.TypeOf<InvalidSymbolException>());
        }
    }
}