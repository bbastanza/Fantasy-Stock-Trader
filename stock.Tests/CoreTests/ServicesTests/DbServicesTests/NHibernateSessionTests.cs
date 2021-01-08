using Core.Services.DbServices;
using NUnit.Framework;

namespace API.Tests.CoreTests.ServicesTests.DbServicesTests
{
    [TestFixture]
    public class NHibernateSessionTests
    {
        [Test]
        public void GetSession_WhenCalled_OpensAndReturnsSession()
        {
            var session = new NHibernateSession();
            var result = session.GetSession();

            Assert.That(result.IsOpen);
        }
    }
}