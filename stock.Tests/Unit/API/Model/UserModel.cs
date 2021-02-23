using API.Models;
using API.Tests.TestUtilities;
using Core.Entities;
using NUnit.Framework;

namespace API.Tests.Unit.API.Model
{
    public class UserModelTests
    {
        [Test]
        public void Constructor_ConstructedWithMultipleHoldings_CreatedWithSameNumberOfHoldings()
        {
            var user = new User(
                TestData.CreateRandomString(),
                TestData.CreateRandomString(), 
                TestData.CreateRandomString()  
                );
            user.Holdings.Add(new Holding());
            user.Holdings.Add(new Holding());
            
            var sut = new UserModel(user);

            Assert.That(sut.Holdings.Count, Is.EqualTo(2));
        }
    }
}