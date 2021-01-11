using System;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using Moq;
using NHibernate;
using NuGet.Frameworks;
using NUnit.Framework;

namespace API.Tests.CoreTests.ServicesTests.UserServicesTests
{
    [TestFixture]
    public class AddUserServiceTests
    {
        private Mock<ISession> _session;
        private User _user;
        private Mock<INHibernateSession> _nHibernateSession;

        [SetUp]
        public void SetUp()
        {
            _session = new Mock<ISession>();
            _user = new User("username", "password", "email");
            _nHibernateSession = new Mock<INHibernateSession>();
        }

        [Ignore("cannot test extension methods with MOQ")]
        [Test]
        public void AddUser_WhenUserAlreadyExists_ThrowsExistingUserException()
        {
            var userName = "username";
            _session
                .Setup(x => x.Query<User>().FirstOrDefault(x => x.UserName == userName))
                .Returns(_user);
            _nHibernateSession.Setup(x => x.GetSession()).Returns(_session.Object);
            var addUserService = new AddUserService(_nHibernateSession.Object);

            Assert.That(() => addUserService.AddUser(userName, "password", "email"),
                Throws.Exception.TypeOf<ExistingUserException>());
        }

        [Test]
        public void AddUser_NoUserExists_ReturnsNewUserSession()
        {
            _nHibernateSession.Setup(x => x.GetSession()).Returns(_session.Object);
            var addUserService = new AddUserService(_nHibernateSession.Object);

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