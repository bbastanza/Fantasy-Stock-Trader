using System.Net.Http;
using API.Tests.TestUtilities;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace API.Tests.Unit.CoreTests.Services.IexServices
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

            Assert.That(() => sut.GetStockBySymbol(TestData.CreateRandomString()), 
                Throws.Exception.TypeOf<IexException>());
        }
    }
}
