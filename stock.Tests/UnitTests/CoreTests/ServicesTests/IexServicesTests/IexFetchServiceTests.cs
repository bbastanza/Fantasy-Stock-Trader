using System.Net.Http;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace API.Tests.UnitTests.CoreTests.ServicesTests.IexServicesTests
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
            var sut = new IexFetchService(_apiHelper.Object, _configuration.Object);

            Assert.That(() => sut.GetStockBySymbol("FAKE"), Throws.Exception.TypeOf<IexException>());
        }
    }
}
