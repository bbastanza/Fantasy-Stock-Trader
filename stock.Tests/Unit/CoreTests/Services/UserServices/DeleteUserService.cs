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
    public class DeleteUserServiceTests
    {
        private Mock<IQueryDbService> _queryDb;

        [SetUp]
        public void SetUp()
        {
            _queryDb = new Mock<IQueryDbService>();
        }

        [Test]
        public void DeleteUser_InvalidPassword_ThrowsUserValidationException()
        {
            _queryDb.Setup(x => x.GetUser("username"))
                .Returns(new User("username", "invalid_password", TestData.CreateRandomString()));
            var sut = new DeleteUserService(_queryDb.Object);

            Assert.That(() => sut.DeleteUser("username", "password"),
                Throws.Exception.TypeOf<UserValidationException>());
        }

        [Test]
        public void DeleteUser_WhenCalled_ReturnsCorrectMessage()
        {
            _queryDb.Setup(x => x.GetUser("username"))
                .Returns(new User("username", "password", TestData.CreateRandomString()));
            var sut = new DeleteUserService(_queryDb.Object);

            var result = sut.DeleteUser("username", "password");

            Assert.That(result, Does.StartWith("username"));
            Assert.That(result, Does.EndWith("database"));
            Assert.That(result, Does.Contain("deleted"));
        }
    }
}