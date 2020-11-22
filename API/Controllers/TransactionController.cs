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
        private readonly ISellService _sellService;
        private string _errorData;

        public TransactionController(ISellService sellService)
        {
            _sellService = sellService;
            _errorData = "new error";
        }

        [HttpPost]
        [Route("sell")]
        public IActionResult Sell(TransactionInputModel transactionInput)
        {
            try
            {
                var transaction = _sellService.SellTransaction(transactionInput);
                return Ok("Sale Valid... UserState: " + JsonSerializer.Serialize(transaction.User));
            }
            catch
            {
                _errorData = "There was an error selling stock";
                return StatusCode(500, _errorData);
            }
        }

        // [HttpPost]
        // [Route("purchase")]
        // public IActionResult Purchase(Transaction transaction)
        // {
        //     try
        //     {
        //         var iexData = _iexFetchService.GetStockBySymbol(transaction.Symbol);
        //         // instead of new UserModel() should look up transactionModel.userName from the DB
        //         var user = new UserModel("Sammy","passk");
        // another purchase shares service *****************
        //         user.PurchaseShares(transaction, iexData.LatestPrice);
        //         user.SetAllocatedFunds(_stockListService.GetStockModelList(user));
        //         return Ok("Purchase Valid... UserState: " + JsonSerializer.Serialize(user));
        //     }
        //     catch
        //     {
        //         _errorData = "There was an error while purchasing";
        //         return StatusCode(500, _errorData);
        //     }
        // }
    }
}