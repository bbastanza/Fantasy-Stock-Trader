using System.Collections.Generic;
using System.Linq;
using API.Tests.TestUtilities;
using Core.Entities;
using Core.Services.DbServices;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace API.Tests.Unit.CoreTests.Services.DbServices
{
    public class DbTests
    {
        private Mock<IUnitOfWork> _nhibernateSession;
        private Mock<ISession> _session;
        private QueryDbService _sut;

        [SetUp]
        public void SetUp()
        {
            _nhibernateSession = new Mock<IUnitOfWork>();

            _session = new Mock<ISession>();

            _session.Setup(x => x.Query<User>())
                .Returns(new List<User>
                {
                    new User(
                        "username", 
                        TestData.CreateRandomString(), 
                        TestData.CreateRandomString())
                }.AsQueryable());

            _session.Setup(x => x.Query<UserSession>())
                .Returns(new List<UserSession>
                {
                    new UserSession()
                }.AsQueryable());

            _nhibernateSession.Setup(x => x.GetSession())
                .Returns(_session.Object);

            _sut = new QueryDbService(_nhibernateSession.Object);
        }

        [Test]
        public void GetUser_WhenCalled_QueryForUser()
        {
            _sut.GetUser("username");
            _session.Verify(x => x.Query<User>());
        }

        [Test]
        public void GetSession_WhenCalled_QueryForUserSession()
        {
            _sut.GetSession("1");
            _session.Verify(x => x.Query<UserSession>());
        }
    }
}