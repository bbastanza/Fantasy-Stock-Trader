using System.Configuration;
using Castle.Core.Configuration;
using Core.Services.IexServices;
using FluentNHibernate.Cfg.Db;
using NUnit.Framework;

namespace API.Tests.IntegrationTests
{
    [TestFixture]
    public class IexCloudTests
    {
        private ApiHelper _apiHelper;

        [SetUp]
        public void SetUp()
        {
            _apiHelper = new ApiHelper();
            // _configuration = new IConfiguration();


        }
        
        [Test]
        public void GetStockBySymbol_WhenCalled_GetsValidIexData()
        {
            // var iexFetchService = new IexFetchService(IApiHelper ApiHelper, IConfiguration configuration)
            //
            // var result = _iexFetchService.GetStockBySymbol("IBM");
            // Assert.That(result.Symbol, Is.EqualTo("IBM"));
            // Assert.That(result.Symbol, Is.EqualTo("International Business Machines Corp."));
            // Assert.That(result.LatestPrice, Is.InstanceOf<double>());
        }
    }
}