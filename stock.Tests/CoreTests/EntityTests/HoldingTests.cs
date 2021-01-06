using System;
using Core.Entities;
using Infrastructure.Exceptions;
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
                TotalShares = 2,
                User = new User()
            };
        }

        [Test]
        public void Sell_SaleAmountLessThanTotal_TotalSharesEqualsTotalSharesMinusInput()
        {
            _holding.Sell(1.5);

            Assert.That(_holding.TotalShares, Is.EqualTo(.5));
        }

        [Test]
        public void Sell_SaleAmountGreaterThanTotal_ThrowsOverDrownHoldingException()
        {
            Assert.That(() => _holding.Sell(3), Throws.TypeOf<OverDrawnHoldingException>());
        }

        [Test]
        public void Purchase_WhenCalled_TotalSharesEqualsTotalSharesPlusArgument()
        {
            _holding.Purchase(1);

            Assert.AreEqual(3, _holding.TotalShares);
        }

        [Test]
        public void SetValue_WhenCalled_ValueEqualsTotalSharesTimesArgument()
        {
            _holding.SetValue(1);

            Assert.AreEqual(2, _holding.Value);
        }

        [Test]
        public void SellAllReturnShareAmount_WhenCalled_ReturnsPreviousTotalSharesAndNewTotalSharesEqualsZero()
        {
            var result = _holding.SellAllReturnShareAmount();

            Assert.AreEqual(2, result);
            Assert.AreEqual(0, _holding.TotalShares);
        }
    }
}