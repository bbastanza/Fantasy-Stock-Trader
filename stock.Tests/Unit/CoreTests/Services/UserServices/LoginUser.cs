using System;
using API.Tests.TestUtilities;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.Unit.CoreTests.Services.UserServices
{
    [TestFixture]
    public class LoginUserTests
    {
        private Mock<IQueryDbService> _queryDb;

        [SetUp]
        public void SetUp()
        {
            _queryDb = new Mock<IQueryDbService>();
        }

        [Test]
        public void Login_NullUser_ThrowsNonExistingUserException()
        {
            _queryDb.Setup(x => x.GetUser("username"))
                .Returns((User) null);
            var sut = new LoginService(_queryDb.Object);

            Assert.That(() => sut.Login("username", "password"),
                Throws.Exception.TypeOf<NonExistingUserException>());
        }

        [Test]
        public void Login_InvalidPassword_ThrowsUserValidationException()
        {
            _queryDb.Setup(x => x.GetUser("username"))
                .Returns(new User("username", "invalid_password", TestData.CreateRandomString()));
            var sut = new LoginService(_queryDb.Object);

            Assert.That(() => sut.Login("username", "password"),
                Throws.Exception.TypeOf<UserValidationException>());
        }

        [Test]
        public void Login_WhenCalled_ReturnsUserSessionWithUser()
        {
            var user = new User(
                "username",
                "password",
                "email"
            );
            _queryDb.Setup(x => x.GetUser("username")).Returns(user);
            var sut = new LoginService(_queryDb.Object);

            var result = sut.Login("username", "password");

            Assert.That(result.SessionId, Is.TypeOf<string>());
            Assert.That(result.InitDateTime, Is.TypeOf<DateTime>());
            Assert.That(result.ExpireDateTime, Is.TypeOf<DateTime>());
            Assert.That(result.User.UserName, Is.EqualTo("username"));
            Assert.That(result.User.Password, Is.EqualTo("password"));
            Assert.That(result.User.Email, Is.EqualTo("email"));
        }
    }
}