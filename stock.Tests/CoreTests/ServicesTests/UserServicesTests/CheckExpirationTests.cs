using System;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.CoreTests.ServicesTests.UserServicesTests
{
    [TestFixture]
    public class CheckExpirationTests
    {
        private Mock<IQueryDb> _queryDb;

        [SetUp]
        public void SetUp()
        {
            _queryDb = new Mock<IQueryDb>();
        }

        [Test]
        public void CheckUserSession_NullSession_ThrowsNonExistingSessionException()
        {
            _queryDb.Setup(x => x.GetSession("1"))
                .Returns((UserSession) null);
            var checkExpiration = new CheckExpiration(_queryDb.Object);

            Assert.That(() => checkExpiration.CheckUserSession("1"),
                Throws.Exception.TypeOf<NonExistingSessionException>());
        }

        [Test]
        public void CheckUserSession_ExpiredSession_ThrowsExpiredSessionException()
        {
            var userSession = new UserSession(){ExpireDateTime = DateTime.Now.Subtract(new TimeSpan(1))};
            _queryDb.Setup(x => x.GetSession("1"))
                .Returns(userSession);
            var checkExpiration = new CheckExpiration(_queryDb.Object);
            
            Assert.That(() => checkExpiration.CheckUserSession("1"),
                Throws.Exception.TypeOf<ExpiredSessionException>());
        }

        [Test]
        public void CheckUserSession_WhenCalled_ReturnsUser()
        {
            var userSession = new UserSession() 
                {
                    User = new User("username", "password", "email"), 
                    ExpireDateTime = DateTime.Now.AddDays(1)
                };
            _queryDb.Setup(x => x.GetSession("1"))
                .Returns(userSession);
            var checkExpiration = new CheckExpiration(_queryDb.Object);

            var result = checkExpiration.CheckUserSession("1");

            Assert.That(result.UserName, Is.EqualTo("username"));
            Assert.That(result.Password, Is.EqualTo("password"));
            Assert.That(result.Email, Is.EqualTo("email"));
        }
    }
}