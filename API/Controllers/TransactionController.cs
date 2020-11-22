using API.ApiServices;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly IApiSellService _apiSellService;
        private readonly IApiPurchaseService _apiPurchaseService;
        private string _errorData;

        public TransactionController(IApiSellService apiSellService, IApiPurchaseService apiPurchaseService)
        {
            _apiSellService = apiSellService;
            _apiPurchaseService = apiPurchaseService;
            _errorData = "new error";
        }

        [HttpPost]
        [Route("sell")]
        public IActionResult Sell(TransactionInputModel transactionInput)
        {
            try
            {
                var transaction = _apiSellService.SellTransaction(transactionInput);
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
                var transaction = _apiPurchaseService.PurchaseTransaction(transactionInput);
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