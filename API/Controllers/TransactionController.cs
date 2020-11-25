using System;
using API.Models;
using Core.Entities.Transactions.TransactionServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly IHandleSaleService _handleSaleService;
        private readonly IHandlePurchaseService _handlePurchaseService;

        public TransactionController(IHandleSaleService handleSaleService, IHandlePurchaseService handlePurchaseService)
        {
            _handleSaleService = handleSaleService;
            _handlePurchaseService = handlePurchaseService;
        }

        [HttpPost]
        [Route("sell")]
        public IActionResult Sell(SaleInputModel saleInput)
        {
            try
            {
                var transaction = _handleSaleService.SellTransaction(saleInput.Amount, saleInput.UserName,
                    saleInput.Symbol, saleInput.SellAll);
                return Ok("Sale Valid... UserState: " + transaction.User);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n \nStackTrace: {ex.StackTrace}");
                return StatusCode(500,  ex.Message);
            }
        }

        [HttpPost]
        [Route("purchase")]
        public IActionResult Purchase(PurchaseInputModel purchaseInput)
        {
            try
            {
                var transaction = _handlePurchaseService.PurchaseTransaction(purchaseInput.Amount,
                    purchaseInput.UserName, purchaseInput.Symbol);
                return Ok("Purchase Valid... UserState: " + transaction.User);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n \nStackTrace: {ex.StackTrace}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}