using API.Models;
using Core.Services.TransactionServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly IHandleSaleService _handleSaleService;
        private readonly IHandlePurchaseService _handlePurchaseService;

        public TransactionController(
            IHandleSaleService handleSaleService,
            IHandlePurchaseService handlePurchaseService)
        {
            _handleSaleService = handleSaleService;
            _handlePurchaseService = handlePurchaseService;
        }

        [HttpPost]
        [Route("sell")]
        public UserModel Sell(SaleInputModel saleInput)
        {
            var transaction = _handleSaleService
                .Sell(saleInput.SessionId, saleInput.ShareAmount, saleInput.Symbol,
                    saleInput.SellAll);

            return new UserModel(transaction.User);
        }

        [HttpPost]
        [Route("purchase")]
        public UserModel Purchase(PurchaseInputModel purchaseInput)
        {
            var transaction = _handlePurchaseService
                .Purchase(purchaseInput.SessionId, purchaseInput.Amount, purchaseInput.Symbol);

            return new UserModel(transaction.User);
        }
    }
}