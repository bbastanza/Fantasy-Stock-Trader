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
            private readonly IIexFetchService _iexFetchService;
            private string _errorData;
            public TransactionController(IIexFetchService iexFetchService)
            {
                _iexFetchService = iexFetchService;
                _errorData =  "new error";
            }
            
            /// <summary>
            /// Post endpoint to SELL stock using iex information and user information
            /// </summary>
            /// <param name="transactionModel">Json TransactionModel including userName, amount & stock abbreviation</param>
            /// <returns>IActionResult with Holding Information or 500 response</returns>
            [Route("sell")]
            public IActionResult Sell(TransactionModel transactionModel)
            {
                try
                {
                    var iexData = _iexFetchService.GetStockByName(transactionModel.Symbol);
                    // instead of new UserModel() should look up transactionModel.userName from the DB
                    var user = new UserModel();
                    user.SellShares(transactionModel,iexData.IexRealtimePrice);
                    return Ok("Sale Valid " + transactionModel.UserName + " UserState " + JsonSerializer.Serialize(user));
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
            /// <returns>IActionResult with Holding Information or 500 response</returns>
            [Route("purchase")]
            public IActionResult Purchase(TransactionModel transactionModel)
            {
                try
                {
                    var iexData = _iexFetchService.GetStockByName(transactionModel.Symbol);
                    // instead of new UserModel() should look up transactionModel.userName from the DB
                    var user = new UserModel();
                    // instead of Holdings[0] should for transactionModle.symbol in the user.Holdings //if symbol doesnt exist create a new holding
                    user.PurchaseShares(transactionModel, iexData.IexRealtimePrice);
                    return Ok("Purchase Valid "  + JsonSerializer.Serialize(transactionModel.UserName) + " HoldingState: " + user.ReadHolding(transactionModel.Symbol));
                }
                catch
                {
                    _errorData = "There was an error while purchasing";
                    return StatusCode(500, _errorData);
                }
            }
    }
}