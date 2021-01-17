using System.IO;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace API.Tests.IntegrationTests.CoreTests.ServiceTests.IexServiceTests
{
    [TestFixture]
    public class IexFetchServiceTests
    {
        private IexFetchService _iexFetchService;

        [SetUp]
        public void SetUp()
        {
            var apiHelper = new ApiHelper();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: false);
            
            _iexFetchService = new IexFetchService(apiHelper, config.Build());
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