using API.Controllers;
using API.Models;
using Core.Entities;
using Core.Services.TransactionServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.APITests.ControllerTests
{
    [TestFixture]
    public class TransactionControllerTests
    {
        private TransactionController _transactionController;

        [SetUp]
        public void SetUp()
        {
            var handleSaleService = new Mock<IHandleSaleService>();
            var handlePurchaseService = new Mock<IHandlePurchaseService>();
            _transactionController = new TransactionController(handleSaleService.Object, handlePurchaseService.Object);
        }

        [Test]
        public void Sell_InvalidInput_ThrowsInvalidInputException()
        {
            Assert.That(() => _transactionController.Sell(new SaleInputModel()),
                Throws.Exception.TypeOf<InvalidInputException>());
        }

        [Test]
        public void Purchase_InvalidInput_ThrowsInvalidInputException()
        {
            Assert.That(() => _transactionController.Purchase(new PurchaseInputModel()),
                Throws.Exception.TypeOf<InvalidInputException>());
        }
    }
}