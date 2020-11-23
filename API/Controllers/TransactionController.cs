using API.Models;
using Core.Entities.Transactions.TransactionServices;
using Microsoft.AspNetCore.Mvc;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly IHandleSaleService _handleSaleService;
        private readonly IHandlePurchaseService _handlePurchaseService;
        private string _errorData;

        public TransactionController(IHandleSaleService handleSaleService, IHandlePurchaseService handlePurchaseService)
        {
            _handleSaleService = handleSaleService;
            _handlePurchaseService = handlePurchaseService;
            _errorData = "new error";
        }

        [HttpPost]
        [Route("sell")]
        public IActionResult Sell(TransactionInputModel transactionInput)
        {
            try
            {
                var transaction = _handleSaleService.SellTransaction(transactionInput.Amount, transactionInput.UserName,
                    transactionInput.Symbol, transactionInput.SellAll);
                return Ok("Sale Valid... UserState: " + JsonSerializer.Serialize(transaction.User));
            }
            catch
            {
                _errorData = "There was an error selling stock";
                return StatusCode(500, _errorData);
            }
        }

        [HttpPost]
        [Route("purchase")]
        public IActionResult Purchase(TransactionInputModel transactionInput)
        {
            try
            {
                var transaction = _handlePurchaseService.PurchaseTransaction(transactionInput.Amount,
                    transactionInput.UserName, transactionInput.Symbol);
                return Ok("Purchase Valid... UserState: " + JsonSerializer.Serialize(transaction.User));
            }
            catch
            {
                _errorData = "There was an error while purchasing";
                return StatusCode(500, _errorData);
            }
        }
    }
}