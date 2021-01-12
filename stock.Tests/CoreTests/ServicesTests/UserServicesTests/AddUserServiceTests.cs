using Core.Entities;
using Core.Services.DbServices;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace API.Tests.CoreTests.ServicesTests.UserServicesTests
{
    [TestFixture]
    public class AddUserServiceTests
    {
        private User _user;
        private Mock<INHibernateSession> _nHibernateSession;
        private Mock<ISession> _session;

        [SetUp]
        public void SetUp()
        {
        }

        [Ignore("cannot test extension methods with MOQ")]
        [Test]
        public void AddUser_WhenUserAlreadyExists_ThrowsExistingUserException()
        {
        }

        [Test]
        public void AddUser_NoUserExists_ReturnsNewUserSession()
        {
        }
    }
}