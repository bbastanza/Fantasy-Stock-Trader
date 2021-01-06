using Core.Services.DbServices;
using NHibernate;
using NHibernate.Hql.Ast.ANTLR.Tree;
using NUnit.Framework;

namespace API.Tests.CoreTests.ServicesTests.DbServicesTests
{
    [TestFixture]
    public class NHibernateSessionTests
    {
        private NHibernateSession _session;

        [SetUp]
        public void SetUp()
        {
            _session = new NHibernateSession();
        }

        [Test]
        public void GetSession_WhenCalled_OpensAndReturnsSession()
        {
            var result = _session.GetSession();

            Assert.That(result, Is.TypeOf<NHibernate.Impl.SessionImpl>());
            Assert.That(result.IsOpen);
        }

        [Test]
        public void CloseSession_WhenCalled_ClosesSession()
        {
            _session.CloseSession();
            
            // Assert.That(_session, Is.NotOpen);
        }
    }
}