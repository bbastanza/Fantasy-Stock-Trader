using API.Mappings;
using API.Models;
using Core.Entities.Transactions;
using Core.Entities.Transactions.TransactionServices;
using Core.Entities.Users.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Core.Models;
using Core.Services;
using Core.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
        [Route("[controller]")]
        public class TransactionController : Controller
        {
            private readonly IIexFetchService _iexFetchService;
            private readonly ISellShareService _sellShareService;
            private readonly ITransactionMapper _transactionMapper;
            private string _errorData;
            
            public TransactionController(IIexFetchService iexFetchService, ISellShareService sellShareService, ITransactionMapper transactionMapper)
            {
                _iexFetchService = iexFetchService;
                _sellShareService = sellShareService;
                _transactionMapper = transactionMapper;
                _errorData =  "new error";
            }
            
            [HttpPost]
            [Route("sell")]
            public IActionResult Sell(TransactionInputModel transactionInput)
            {
                try
                {
                    var iexData = _iexFetchService.GetStockBySymbol(transactionInput.Symbol);
                    var transaction = _transactionMapper.MapTransaction(transactionInput, iexData);
                     transaction.User = _sellShareService.SellShares(transaction);
                        // should be another new service pass in user get back user with updated allocation    
                    return Ok("Sale Valid... UserState: " /*+ JsonSerializer.Serialize(transactionInput.User)*/);
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
