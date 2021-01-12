using Core.Entities;
using Core.Services.DbServices;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.CoreTests.ServicesTests.UserServicesTests
{
    [TestFixture]
    public class DeleteUserServiceTests
    {
        private Mock<IQueryDb> _queryDb;

        [SetUp]
        public void SetUp()
        {
            _queryDb = new Mock<IQueryDb>();
        }

        [Test]
        public void DeleteUser_InvalidPassword_ThrowsUserValidationException()
        {
            _queryDb.Setup(x => x.GetUser("username"))
                .Returns(new User("username", "invalid_password", "email"));
            var deleteUserService = new DeleteUserService(_queryDb.Object);

            Assert.That(() => deleteUserService.DeleteUser("username", "password"),
                Throws.Exception.TypeOf<UserValidationException>());
        }

        [Test]
        public void DeleteUser_WhenCalled_ReturnsCorrectMessage()
        {
            _queryDb.Setup(x => x.GetUser("username"))
                .Returns(new User("username", "password", "email"));
            var deleteUserService = new DeleteUserService(_queryDb.Object);

            var result = deleteUserService.DeleteUser("username", "password");

            Assert.That(result, Does.StartWith("username"));
            Assert.That(result, Does.EndWith("database"));
            Assert.That(result, Does.Contain("deleted"));
        }
    }
}