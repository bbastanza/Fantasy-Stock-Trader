using System.Collections.Generic;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Core.Models;
using Core.Services;
using Infrastructure;
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
                    var iexData = _iexFetchService.GetStockBySymbol(transactionModel.Symbol);
                    // instead of new UserModel() should look up transactionModel.userName from the DB
                    
                    // i think this might work, but it doesn't work yet because really I wont be creating a new user from here
                    // var user = new UserModel(new UserModel(){UserName = "Brian", Password = "password"});
                    var user = new UserModel("brian", "password");
                    var userInfrastructure = new TransactionInfrastructure(user);
                    user.SellShares(transactionModel,iexData.IexRealtimePrice);
                    user.SetAllocatedDollars(userInfrastructure.GetUserSymbols());
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
       
                    var iexData = _iexFetchService.GetStockBySymbol(transactionModel.Symbol);
                    // instead of new UserModel() should look up transactionModel.userName from the DB
                    var user = new UserModel("Sammy","passk");
                    var userInfrastructure = new TransactionInfrastructure(user);
                    user.PurchaseShares(transactionModel, iexData.IexRealtimePrice);
                    user.SetAllocatedDollars(userInfrastructure.GetUserSymbols());
                    return Ok("Purchase Valid " + transactionModel.UserName + " UserState " + JsonSerializer.Serialize(user));
                }
                catch
                {
                    _errorData = "There was an error while purchasing";
                    return StatusCode(500, _errorData);
                }
            }





        }
}