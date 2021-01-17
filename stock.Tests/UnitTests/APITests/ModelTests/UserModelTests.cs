using API.Models;
using Core.Entities;
using NUnit.Framework;

namespace API.Tests.UnitTests.APITests.ModelTests
{
    public class UserModelTests
    {
        [Test]
        public void Constructor_ConstructedWithMultipleHoldings_CreatedWithSameNumberOfHoldings()
        {
            var user = new User("username", "password", "email");
            user.Holdings.Add(new Holding());
            user.Holdings.Add(new Holding());
            
            var sut = new UserModel(user);

            Assert.That(sut.Holdings.Count, Is.EqualTo(user.Holdings.Count));
        }
    }
}