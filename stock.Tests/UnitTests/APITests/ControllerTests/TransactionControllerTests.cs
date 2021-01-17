using API.Controllers;
using API.Models;
using Core.Services.TransactionServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.UnitTests.APITests.ControllerTests
{
    [TestFixture]
    public class TransactionControllerTests
    {
        private TransactionController _sut;

        [SetUp]
        public void SetUp()
        {
            var handleSaleService = new Mock<IHandleSaleService>();
            var handlePurchaseService = new Mock<IHandlePurchaseService>();
            _sut = new TransactionController(handleSaleService.Object, handlePurchaseService.Object);
        }

        [Test]
        public void Sell_InvalidInput_ThrowsInvalidInputException()
        {
            Assert.That(() => _sut.Sell(new SaleInputModel()),
                Throws.Exception.TypeOf<InvalidInputException>());
        }

        [Test]
        public void Purchase_InvalidInput_ThrowsInvalidInputException()
        {
            Assert.That(() => _sut.Purchase(new PurchaseInputModel()),
                Throws.Exception.TypeOf<InvalidInputException>());
        }
    }
}