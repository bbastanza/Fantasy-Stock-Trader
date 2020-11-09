using JsonSerializer = System.Text.Json.JsonSerializer;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

        [ApiController]
        [Route("[controller]")]
        public class TransactionController : Controller
        {
            private readonly IJsonStockService _jsonStockService;
            private string _errorData;
            public TransactionController(IJsonStockService jsonStockService)
            {
                _jsonStockService = jsonStockService;
                _errorData =  "new error";
            }
            
            /// <summary>
            /// Post endpoint to SELL stock using iex information and user information
            /// </summary>
            /// <param name="transactionModel">Json TransactionModel including userName, amount & stock abbreviation</param>
            /// <returns>IActionResult with success code or 500 response</returns>

            [Route("sell")]
            public IActionResult Sell(TransactionModel transactionModel)
            {
                try
                {
                    var iexData = _jsonStockService.GetStockByName(transactionModel.Symbol);
                    // instead of new UserModel() should look up transactionModel.userName from the DB
                    var user = new UserModel();
                    // should sell from a holding that the user has... cannot sell a holding that doesn't exits or of a overage amount.
                    user.Holdings[0].PurchaseShares(transactionModel.Amount,iexData.IexRealtimePrice);
                    return Ok("Sale Valid.... HoldingState: " + JsonSerializer.Serialize(user.Holdings[0]));
                    
                }
                catch
                {
                    return StatusCode(500, _errorData);
                }
            }
        
            /// <summary>
            /// Post endpoint to PURCHASE stock using iex information and user information
            /// </summary>
            /// <param name="transactionModel">Json TransactionModel including userName, amount & stock abbreviation</param>
            /// <returns>IActionResult with success code or 500 response</returns>
            [Route("purchase")]
            public IActionResult Purchase(TransactionModel transactionModel)
            {
                // returning message of it working or not working
                try
                {
                    var iexData = _jsonStockService.GetStockByName(transactionModel.Symbol);
                    // instead of new UserModel() should look up transactionModel.userName from the DB
                    var user = new UserModel();
                    // instead of Holdings[0] should for transactionModle.symbol in the user.Holdings //if symbol doesnt exist create a new holding
                    user.Holdings[0].SellShares(transactionModel.Amount,iexData.IexRealtimePrice);
                    return Ok("Purchase Valid.... HoldingState: " + JsonSerializer.Serialize(user.Holdings[0]));
                }
                catch
                {
                    _errorData = "There was an error while purchasing";
                    return StatusCode(500, _errorData);
                }

            }
    }
}