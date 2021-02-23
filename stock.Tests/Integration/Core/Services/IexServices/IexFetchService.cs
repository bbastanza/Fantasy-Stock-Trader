using API.Tests.TestUtilities;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using NUnit.Framework;

namespace API.Tests.Integration.Core.Services.IexServices
{
    [TestFixture]
    public class IexFetchServiceTests
    {
        private IexFetchService _iexFetchService;

        [SetUp]
        public void SetUp()
        {
            var apiHelper = new ApiHelper();

            _iexFetchService = new IexFetchService(apiHelper, TestData.Configuration);
        }

        [Test]
        public void GetStockBySymbol_ValidSymbol_GetsValidIexData()
        {
            var result = _iexFetchService.GetStockBySymbol("IBM");

            Assert.That(result.Symbol, Is.EqualTo("IBM"));
            Assert.That(result.CompanyName, Is.EqualTo("International Business Machines Corp."));
            Assert.That(result.LatestPrice, Is.InstanceOf<double>());
        }

        [Test]
        public void GetStockBySymbol_InvalidSymbol_ThrowsIexException()
        {
            Assert.That(() => _iexFetchService.GetStockBySymbol("XYZ123"), Throws.Exception.TypeOf<IexException>());
        }
    }
}