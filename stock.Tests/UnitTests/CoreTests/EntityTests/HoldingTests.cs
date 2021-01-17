using Core.Entities;
using Infrastructure.Exceptions;
using NUnit.Framework;

namespace API.Tests.UnitTests.CoreTests.EntityTests
{
    public class HoldingTests
    {
        private Holding _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Holding()
            {
                Symbol = "FAKE",
                CompanyName = "Fake Stock",
                Value = 10,
                TotalShares = 2,
                User = new User()
            };
        }

        [Test]
        public void Sell_SaleAmountLessThanTotal_TotalSharesEqualsTotalSharesMinusInput()
        {
            _sut.Sell(1.5);

            Assert.That(_sut.TotalShares, Is.EqualTo(.5));
        }

        [Test]
        public void Sell_SaleAmountGreaterThanTotal_ThrowsOverDrownHoldingException()
        {
            Assert.That(() => _sut.Sell(3), Throws.TypeOf<OverDrawnHoldingException>());
        }

        [Test]
        public void Purchase_WhenCalled_TotalSharesEqualsTotalSharesPlusArgument()
        {
            _sut.Purchase(1);

            Assert.AreEqual(3, _sut.TotalShares);
        }

        [Test]
        public void SetValue_WhenCalled_ValueEqualsTotalSharesTimesArgument()
        {
            _sut.SetValue(1);

            Assert.AreEqual(2, _sut.Value);
        }

        [Test]
        public void SellAllReturnShareAmount_WhenCalled_ReturnsTotalShares()
        {
            var result = _sut.SellAllReturnShareAmount();

            Assert.AreEqual(2, result);
        }
        
        [Test]
        public void SellAllReturnShareAmount_WhenCalled_TotalSharesSetToZero()
        {
            _sut.SellAllReturnShareAmount();

            Assert.AreEqual(0, _sut.TotalShares);
        }
    }
}