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
            var user = new User("username", "password", "email");
            
            var handleSaleService = new Mock<IHandleSaleService>();
            handleSaleService
                .Setup(x => x.Sell("1", 1, "FAKE", false))
                .Returns(new Transaction() {User = user});

            var handlePurchaseService = new Mock<IHandlePurchaseService>();
            handlePurchaseService
                .Setup(x => x.Purchase("1", 1, "FAKE"))
                .Returns(new Transaction() {User = user});

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

        [Test]
        public void Sell_WhenCalled_ReturnsTypeUserModel()
        {
            var saleInput = new SaleInputModel() {SessionId = "1", Symbol = "FAKE", ShareAmount = 1, SellAll = false};
            
            var result = _transactionController.Sell(saleInput);

            Assert.That(result, Is.TypeOf<UserModel>());
        }

        [Test]
        public void Purchase_WhenCalled_ReturnsTypeUserModel()
        {
            var purchaseInput = new PurchaseInputModel() {SessionId = "1", Symbol = "FAKE", Amount = 1};
            
            var result = _transactionController.Purchase(purchaseInput);

            Assert.That(result, Is.TypeOf<UserModel>());
        }
    }
}