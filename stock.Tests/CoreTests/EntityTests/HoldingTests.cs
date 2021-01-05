using Core.Entities;
using NUnit.Framework;

namespace API.Tests.CoreTests.EntityTests
{
    public class HoldingTests
    {
        private Holding _holding;

        [SetUp]
        public void Setup()
        {
            _holding = new Holding()
            {
                Symbol = "IBM", 
                CompanyName = "International Business Machines", 
                Value = 10, 
                TotalShares = 1,
                User = new User()
            };
        }

        [Test]
        public void SellAndReturnIsOverdrawn_ValidAmountToSell_ReturnFalse()
        {
            var result = _holding.SellAndReturnIsOverdrawn(1);

            Assert.IsFalse(result);
        }
        
        [Test]
        public void SellAndReturnIsOverdrawn_InvalidAmountToSell_ReturnTrue()
        {
            var result = _holding.SellAndReturnIsOverdrawn(2);

            Assert.IsTrue(result);
        }
        
        [Test]
        public void Purchase_WhenCalled_TotalSharesEqualsTotalSharesPlusArgument()
        {
            _holding.Purchase(1);

            Assert.AreEqual(2, _holding.TotalShares);
        }
        
        [Test]
        public void SetValue_WhenCalled_ValueEqualsTotalSharesTimesArgument()
        {
            _holding.SetValue(1);

            Assert.AreEqual(1, _holding.Value);
        }
        
        [Test]
        public void SellAll_WhenCalled_TotalShareEqualsZero()
        {
            _holding.SellAll();

            Assert.AreEqual(0, _holding.TotalShares);
        }
        
        [Test]
        public void SellAll_WhenCalled_ReturnTotalShares()
        {
            var result = _holding.SellAll();

            Assert.AreEqual(1, result);
        }
    }
}