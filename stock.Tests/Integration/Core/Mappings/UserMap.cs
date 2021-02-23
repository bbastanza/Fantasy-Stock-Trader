using System;
using API.Tests.TestUtilities;
using Core.Entities;
using Core.Services.DbServices;
using FluentNHibernate.Testing;
using NHibernate;
using NUnit.Framework;

namespace API.Tests.Integration.Core.Mappings
{
    [TestFixture]
    public class UserMap
    {
        private ISession _session;

        [SetUp]
        public void SetUp()
        {
            _session = new NHibernateSessionFactory(TestData.Configuration).OpenSession();
        }

        [Test]
        public void UserMap_WhenCalled_MapsUserToUserColumn()
        {
            using ITransaction itransaction = _session.BeginTransaction();
            
            new PersistenceSpecification<User>(_session)
                .CheckProperty(x => x.UserName, "brian")
                .CheckProperty(x => x.Password, "password")
                .CheckProperty(x => x.Email, "email")
                .CheckProperty(x => x.CreatedAt, DateTime.Today)
                .CheckProperty(x => x.Balance, 1.11)
                .VerifyTheMappings();
            
            itransaction.RollbackAsync();
        }

        [Test]
        public void Should_Map_Has_Many_Transactions()
        {
            var user = new User("username", "password", "email");
            using ITransaction itransaction = _session.BeginTransaction();

            var transaction = new Transaction {User = user};

            _session.Save(user);
            _session.Flush();
            _session.Refresh(transaction);
            
            Assert.AreEqual(transaction.User, user);
                
            itransaction.RollbackAsync();
        }

        [Test]
        public void Should_Map_Has_Many_UserSessions()
        {
            using ITransaction itransaction = _session.BeginTransaction();
            var user = new User("username", "password", "email");

            var userSession = new UserSession {User = user};

            _session.Save(user);
            _session.Flush();
            _session.Refresh(userSession);
            
            Assert.AreEqual(userSession.User, user);
                
            itransaction.RollbackAsync();
        }
        
        [Test]
        public void Should_Map_Has_Many_Holdings()
        {
            using ITransaction itransaction = _session.BeginTransaction();
            var user = new User("username", "password", "email");

            var holding = new Holding {User = user};

            _session.Save(user);
            _session.Flush();
            _session.Refresh(holding);
            
            Assert.AreEqual(holding.User, user);
                
            itransaction.RollbackAsync();
        }
    }
}