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
        public UserModel Sell(TransactionInputModel transactionInput)
        {
            var transaction = _handleSaleService
                .Sell(transactionInput.SessionId, transactionInput.Amount, transactionInput.Symbol,
                    transactionInput.SellAll);

            return new UserModel(transaction.User);
        }

        [HttpPost]
        [Route("purchase")]
        public UserModel Purchase(TransactionInputModel transactionInput)
        {
            var transaction = _handlePurchaseService
                .Purchase(transactionInput.SessionId, transactionInput.Amount, transactionInput.Symbol);

            return new UserModel(transaction.User);
        }
    }
}