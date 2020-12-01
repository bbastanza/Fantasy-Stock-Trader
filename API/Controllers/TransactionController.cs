using System;
using System.Linq;
using API.Models;
using API.OutputMappings;
using Core.Services.TransactionServices;
using Infrastructure.Exceptions;
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
        private readonly IUserOutputMap _userOutputMap;

        public TransactionController(
            IHandleSaleService handleSaleService, 
            IHandlePurchaseService handlePurchaseService, 
            IUserOutputMap userOutputMap)
        {
            _handleSaleService = handleSaleService;
            _handlePurchaseService = handlePurchaseService;
            _userOutputMap = userOutputMap;
        }

        [HttpPost]
        [Route("sell")]
        public IActionResult Sell(SaleInputModel saleInput)
        {
            try
            {
                var transaction = _handleSaleService.SellTransaction(saleInput.Amount, saleInput.UserName,
                    saleInput.Symbol, saleInput.SellAll);
                var userOutput = _userOutputMap.MapUserOutput(transaction.User);
                return Ok("Sale Valid... UserState: " + userOutput );
            }
            catch (DreamTraderException ex)
            {
                Console.WriteLine($"DreamTraderException => {ex.Path}\n");
                return StatusCode(409, $"{ex.Message} | {ex.Path}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n \nStackTrace: {ex.StackTrace}");
                return StatusCode(500,  ex.Message);
            }
        }

        [HttpPost]
        [Route("purchase")]
        public IActionResult Purchase(PurchaseInputModel purchaseInput)
        {
            try
            {
                var transaction = _handlePurchaseService.PurchaseTransaction(purchaseInput.Amount,
                    purchaseInput.UserName, purchaseInput.Symbol);
                var userOutput = _userOutputMap.MapUserOutput(transaction.User);
                return Ok($"Purchase Valid... UserState: {userOutput}");
            }
            catch (DreamTraderException ex)
            {
                Console.WriteLine($"DreamTraderException => {ex.Path}\n");
                return StatusCode(409, $"{ex.Message} | {ex.Path} | {ex.Method}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n \nStackTrace: {ex.StackTrace}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}