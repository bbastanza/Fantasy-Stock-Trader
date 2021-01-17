using System.IO;
using Core.Services.IexServices;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace API.Tests.IntegrationTests
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
                .AddJsonFile("stock.Tests/IntegrationTests/appsettings.Test.json", optional: false, reloadOnChange: false);
            
            _iexFetchService = new IexFetchService(apiHelper, config.Build());
        }

        [Ignore("cannot find appsettings.Test.json")]
        [Test]
        public void GetStockBySymbol_WhenCalled_GetsValidIexData()
        {
            // "Tpk_78fdb3ab586845aba561ebf6c43c96e0"
            // _configurationSection.Setup(x => x.Path).Returns("IexKeys");
            // _configurationSection.Setup(x => x.Key).Returns("TestKey");
            // _configurationSection.Setup(x => x.Value).Returns("Tpk_78fdb3ab586845aba561ebf6c43c96e0");
            // _configuration.Setup(a => a.GetSection(It.Is<string>(s => s == "IexKeys:TestKey")))
            //     .Returns(_configurationSection.Object);

            var result = _iexFetchService.GetStockBySymbol("IBM");

            Assert.That(result.Symbol, Is.EqualTo("IBM"));
            Assert.That(result.Symbol, Is.EqualTo("International Business Machines Corp."));
            Assert.That(result.LatestPrice, Is.InstanceOf<double>());
        }
    }
}