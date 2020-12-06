using System;
using API.Models;
using API.OutputMappings;
using Core.Services.TransactionServices;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

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
        public UserModel Sell(SaleInputModel saleInput)
        {
                var transaction = _handleSaleService
                    .SellTransaction(saleInput.Amount, saleInput.UserName, saleInput.Symbol, saleInput.SellAll);
                
                return _userOutputMap.MapUserOutput(transaction.User);
        }

        [HttpPost]
        [Route("purchase")]
        public UserModel Purchase(PurchaseInputModel purchaseInput)
        {
                var transaction = _handlePurchaseService
                    .PurchaseTransaction(purchaseInput.Amount, purchaseInput.UserName, purchaseInput.Symbol);
                
                return _userOutputMap.MapUserOutput(transaction.User);
        }
    }
}