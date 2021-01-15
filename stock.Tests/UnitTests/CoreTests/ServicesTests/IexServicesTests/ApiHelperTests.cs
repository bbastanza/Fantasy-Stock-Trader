using Core.Services.IexServices;
using NUnit.Framework;

namespace API.Tests.UnitTests.CoreTests.ServicesTests.IexServicesTests
{
    [TestFixture]
    public class ApiHelperTests
    {
        [Test]
        public void Constructor_WhenCalled_CreatesHttpClientWithCorrectHeaders()
        {
            var apiHelper = new ApiHelper();
            
            Assert.That(apiHelper.ApiClient.DefaultRequestHeaders.ToString(), Does.Match("application/json"));
        }
    }
}