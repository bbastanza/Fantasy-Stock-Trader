using Core.Helpers;
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
            private readonly ITransactionHelper _transactionHelper;
            private string _errorData;
            
            public TransactionController(IIexFetchService iexFetchService, ITransactionHelper transactionHelper)
            {
                _iexFetchService = iexFetchService;
                _transactionHelper = transactionHelper;
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
                    user.SetAllocatedFunds(_transactionHelper.GetStockModelList(user));
                    return Ok("Sale Valid... UserState: " + JsonSerializer.Serialize(user));
                }
                catch
                {
                    _errorData = "There was an error selling stock";
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
                    user.SetAllocatedFunds(_transactionHelper.GetStockModelList(user));
                    return Ok("Purchase Valid... UserState: " + JsonSerializer.Serialize(user));
                }
                catch
                {
                    _errorData = "There was an error while purchasing";
                    return StatusCode(500, _errorData);
                }
            }
        }
}
