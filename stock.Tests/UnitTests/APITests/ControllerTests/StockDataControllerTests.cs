using API.Controllers;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.UnitTests.APITests.ControllerTests
{
    [TestFixture]
    public class StockDataControllerTests
    {
        private StockDataController _sut;

        [SetUp]
        public void SetUp()
        {
            var iexFetchService = new Mock<IIexFetchService>();
            _sut = new StockDataController(iexFetchService.Object);
        }
        
        [Test]
        public void GetStockData_NullStockSymbol_ThrowsInvalidSymbolException()
        {
            Assert.That(() => _sut.GetStockData(null),
                Throws.Exception.TypeOf<InvalidSymbolException>());
        }
    }
}