using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Core.Entities;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace API.Tests.CoreTests.ServicesTests.IexServicesTests
{
    [TestFixture]
    public class IexFetchServiceTests
    {
        private Mock<IApiHelper> _apiHelper;
        private Mock<IConfiguration> _configuration;

        [SetUp]
        public void SetUp()
        {
            _apiHelper = new Mock<IApiHelper>();
            _configuration = new Mock<IConfiguration>();
        }

        [Test]
        public void GetStockBySymbol_UnsuccessfulFromIex_ThrowsInvalidSymbolException()
        {
            _apiHelper.Object.ApiClient = new HttpClient();
            var iexFetchService = new IexFetchService(_apiHelper.Object, _configuration.Object);

            Assert.That(() => iexFetchService.GetStockBySymbol("FAKE"), Throws.Exception.TypeOf<IexException>());
        }

        [Test]
        public void UpdateHolding_UnsuccessfulFromIex_ThrowsInvalidSymbolException()
        {
            var holding = new Holding() {Symbol = "FAKE", CompanyName = "Fake Company", TotalShares = 2};
            _apiHelper.Object.ApiClient = new HttpClient();
            var iexFetchService = new IexFetchService(_apiHelper.Object, _configuration.Object);

            Assert.That(() => iexFetchService.UpdateHolding(holding), Throws.Exception.TypeOf<IexException>());
        }

        [Ignore("broken test")]
        [Test]
        public void UpdateHolding_WhenCalled_UpdatesHoldingData()
        {
            // var holding = new Holding() {Symbol = "FAKE", CompanyName = "Fake Company", TotalShares = 2};
            // var httpClient = new Mock<HttpClient>();
            // httpClient.Setup(x => x.GetAsync("FAKE"))
            //     .ReturnsAsync(Task < new HttpResponseMessage()
            //     {
            //         StatusCode = HttpStatusCode.Accepted,
            //         Content = JsonSerializer.Serialize(new IexStock()
            //             {Symbol = "FAKE", CompanyName = "Fake Company", LatestPrice = 5})
            //     } >); 
            // _apiHelper.Object.ApiClient = new HttpClient();
            // var iexFetchService = new IexFetchService(_apiHelper.Object, _configuration.Object);
            //
            // Assert.That(() => iexFetchService.UpdateHolding(holding), Throws.Exception.TypeOf<IexException>());
        }
    }
}