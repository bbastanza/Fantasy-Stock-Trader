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
            var sut = new ApiHelper();
            
            Assert.That(sut.ApiClient.DefaultRequestHeaders.ToString(), Does.Match("application/json"));
        }
    }
}