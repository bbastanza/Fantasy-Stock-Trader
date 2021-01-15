using System;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.UnitTests.CoreTests.ServicesTests.UserServicesTests
{
    [TestFixture]
    public class AddUserServiceTests
    {
        private User _user;
        private Mock<IQueryDbService> _queryDb;

        [SetUp]
        public void SetUp()
        {
            _user = new User("username", "password", "email");
            _queryDb = new Mock<IQueryDbService>();
        }

        [Test]
        public void AddUser_WhenUserAlreadyExists_ThrowsExistingUserException()
        {
            _queryDb.Setup(x => x.GetUser("username")).Returns(_user);
            var addUserService = new AddUserService(_queryDb.Object);

            Assert.That(() => addUserService.AddUser("username", "password", "email"),
                Throws.Exception.TypeOf<ExistingUserException>());
        }

        [Test]
        public void AddUser_NoUserExists_ReturnsUserSessionWithNewUser()
        {
            _queryDb.Setup(x => x.GetUser("username")).Returns((User) null);
            var addUserService = new AddUserService(_queryDb.Object);

            var result = addUserService.AddUser("username", "password", "email");

            Assert.That(result.SessionId, Is.TypeOf<string>());
            Assert.That(result.InitDateTime, Is.TypeOf<DateTime>());
            Assert.That(result.ExpireDateTime, Is.TypeOf<DateTime>());
            Assert.That(result.User.UserName, Is.EqualTo("username"));
            Assert.That(result.User.Password, Is.EqualTo("password"));
            Assert.That(result.User.Email, Is.EqualTo("email"));
        }
    }
}