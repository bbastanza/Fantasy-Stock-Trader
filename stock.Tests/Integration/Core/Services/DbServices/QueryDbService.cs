using System.Linq;
using API.Tests.TestUtilities;
using Core.Entities;
using Core.Services.DbServices;
using NHibernate;
using NUnit.Framework;

namespace API.Tests.Integration.Core.Services.DbServices
{
    public class DbQueryServiceTests
    {
        private QueryDbService _sut;
        private UnitOfWork _unitOfWork;
        private User _user;
        private UserSession _userSession;

        [SetUp]
        public void SetUp()
        {
            _user = new User("username", "password", "email");
            _userSession = new UserSession {SessionId = "1"};
            
            var sessionFactory = new NHibernateSessionFactory(TestData.Configuration);
            
            _unitOfWork = new UnitOfWork(sessionFactory);

            _sut = new QueryDbService(_unitOfWork);
        }

        [Test]
        public void GetUser_ValidUsername_ReturnsUser()
        {
            var session = _unitOfWork.GetSession();
            using ITransaction transaction = session.BeginTransaction();
            session.Save(_user);

            var result = _sut.GetUser("username");

            Assert.That(result.UserName, Is.EqualTo("username"));

            transaction.RollbackAsync();
        }

        [Test]
        public void GetUser_InvalidUsername_ReturnsNull()
        {
            var session = _unitOfWork.GetSession();
            using ITransaction transaction = session.BeginTransaction();
            session.Save(_user);

            var result = _sut.GetUser("InvalidUsername");

            Assert.That(result, Is.Null);

            transaction.RollbackAsync();
        }

        [Test]
        public void GetSession_WhenCalled_ReturnsUserSession()
        {
            var session = _unitOfWork.GetSession();
            using ITransaction transaction = session.BeginTransaction();
            session.Save(_userSession);

            var result = _sut.GetSession("1");

            Assert.That(result.SessionId, Is.EqualTo("1"));

            transaction.RollbackAsync();
        }

        [Test]
        public void GetSession_InvalidSessionId_ReturnsNull()
        {
            var session = _unitOfWork.GetSession();
            using ITransaction transaction = session.BeginTransaction();
            session.Save(_userSession);

            var result = _sut.GetSession("2");

            Assert.That(result, Is.Null);

            transaction.RollbackAsync();
        }

        [Test]
        public void DeleteUser_ValidUsername_DeletesFromDb()
        {
            var session = _unitOfWork.GetSession();
            using ITransaction transaction = session.BeginTransaction();
            session.Save(_user);

            _sut.DeleteUser("username");

            var result = session.Query<User>().FirstOrDefault(x => x.UserName == "username");
            Assert.That(result, Is.Null);

            transaction.RollbackAsync();
        }

        [Test]
        public void DeleteUser_InvalidUsername_DoesNotDeleteFromDb()
        {
            var session = _unitOfWork.GetSession();
            using ITransaction transaction = session.BeginTransaction();
            session.Save(_user);

            _sut.DeleteUser("InvalidUserName");

            var result = session.Query<User>().FirstOrDefault(x => x.UserName == "username");
            Assert.That(result, Is.Not.Null);

            transaction.RollbackAsync();
        }

        [Test]
        public void DeleteSession_ValidSessionId_ReturnsNull()
        {
            var session = _unitOfWork.GetSession();
            using ITransaction transaction = session.BeginTransaction();
            session.Save(_userSession);

            _sut.DeleteSession("1");

            var result = session.Query<UserSession>().FirstOrDefault(x => x.SessionId == "1");
            Assert.That(result, Is.Null);

            transaction.RollbackAsync();
        }

        [Test]
        public void DeleteSession_InvalidSessionId_DoesNotDeleteFromDb()
        {
            var session = _unitOfWork.GetSession();
            using ITransaction transaction = session.BeginTransaction();
            session.Save(_userSession);

            _sut.DeleteSession("InvalidUserSession");

            var result = session.Query<UserSession>().FirstOrDefault(x => x.SessionId == "1");
            Assert.That(result, Is.Not.Null);

            transaction.RollbackAsync();
        }

        [Test]
        public void SaveToDb_WhenCalledWithUser_SavesUser()
        {
            var session = _unitOfWork.GetSession();
            using ITransaction transaction = session.BeginTransaction();

            _sut.SaveToDb(_user);

            var result = session.Query<User>().FirstOrDefault(x => x.UserName == "username");
            Assert.That(result.UserName, Is.EqualTo("username"));

            transaction.RollbackAsync();
        }
    }
}