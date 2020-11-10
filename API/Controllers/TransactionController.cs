using System;
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
            /// <returns>IActionResult with Holding Information or 500 response</returns>

            [Route("sell")]
            public IActionResult Sell(TransactionModel transactionModel)
            {
                try
                {
                    var iexData = _jsonStockService.GetStockByName(transactionModel.Symbol);
                    // instead of new UserModel() should look up transactionModel.userName from the DB
                    var user = new UserModel();
                    // should sell from a holding that the user has... cannot sell a holding that doesn't exits or of a overage amount.
                    user.Holdings[0].SellShares(transactionModel.Amount,iexData.IexRealtimePrice);
                    var holding = user.Holdings[0].ReadHolding();
                    return Ok("Sale Valid " + transactionModel.UserName + " HoldingState: " + holding);
                }
                catch
                {
                    return StatusCode(500, _errorData);
                }
            }
            
            /// <summary>
            /// Post endpoint to SELL all stock of a certain symbol using iex information and user information
            /// </summary>
            /// <param name="transactionModel">Json TransactionModel including userName, {sellAll = true} & stock abbreviation</param>
            /// <returns>IActionResult with Holding Information or 500 response</returns>

            [Route("sellAll")]
            public IActionResult SellAll(TransactionModel transactionModel)
            {
                try
                {
                    var iexData = _jsonStockService.GetStockByName(transactionModel.Symbol);
                    // instead of new UserModel() should look up transactionModel.userName from the DB
                    var user = new UserModel();
                    // should sell from a holding that the user has... cannot sell a holding that doesn't exits or of a overage amount.
                    user.Holdings[0].PurchaseShares(transactionModel.Amount,iexData.IexRealtimePrice);
                    var holding = user.Holdings[0].ReadHolding();
                    return Ok("Sale Valid " + transactionModel.UserName + " HoldingState: " + holding);
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
                    var iexData = _jsonStockService.GetStockByName(transactionModel.Symbol);
                    // instead of new UserModel() should look up transactionModel.userName from the DB
                    var user = new UserModel();
                    // instead of Holdings[0] should for transactionModle.symbol in the user.Holdings //if symbol doesnt exist create a new holding
                    user.Holdings[0].PurchaseShares(transactionModel.Amount,iexData.IexRealtimePrice);
                    var holding = user.Holdings[0].ReadHolding();
                    return Ok("Purchase Valid "  + JsonSerializer.Serialize(transactionModel.UserName) + " HoldingState: " + holding);
                }
                catch
                {
                    _errorData = "There was an error while purchasing";
                    return StatusCode(500, _errorData);
                }

            }
    }
}