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
            
            [HttpPost]
            [Route("sell")]
            public IActionResult Sell(TransactionModel transactionModel)
            {
                try
                {
                    var iexData = _iexFetchService.GetStockBySymbol(transactionModel.Symbol);
                    // instead of new UserModel() should look up transactionModel.userName from the DB
                    var user = new UserModel("brian", "password");
                    user.SellShares(transactionModel,iexData.LatestPrice);
                    var userInfrastructure = new TransactionInfrastructure(user,_iexFetchService);
                    user.SetAllocatedDollars(userInfrastructure.StockModelList);
                    return Ok("Sale Valid " + transactionModel.UserName + " UserState " + JsonSerializer.Serialize(user));
                }
                catch
                {
                    return StatusCode(500, _errorData);
                }
            }
            
            [HttpPost]
            [Route("purchase")]
            public IActionResult Purchase(TransactionModel transactionModel)
            {
                try
                {
       
                    var iexData = _iexFetchService.GetStockBySymbol(transactionModel.Symbol);
                    // instead of new UserModel() should look up transactionModel.userName from the DB
                    var user = new UserModel("Sammy","passk");
                    user.PurchaseShares(transactionModel, iexData.LatestPrice);
                    var userInfrastructure = new TransactionInfrastructure(user,_iexFetchService);
                    user.SetAllocatedDollars(userInfrastructure.StockModelList);
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
