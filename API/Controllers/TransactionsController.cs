using System.IO;
using API.Models;
using Core.Services.TransactionServices;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        private readonly IHandleSaleService _handleSaleService;
        private readonly IHandlePurchaseService _handlePurchaseService;
        private readonly string _path;

        public TransactionsController(
            IHandleSaleService handleSaleService,
            IHandlePurchaseService handlePurchaseService)
        {
            _handleSaleService = handleSaleService;
            _handlePurchaseService = handlePurchaseService;
            _path = Path.GetFullPath(ToString()!);
        }

        [HttpPost]
        [Route("sell")]
        public void Sell(SaleInputModel saleInput)
        {
            if (saleInput.SessionId == null || saleInput.Symbol == null ||
                saleInput.ShareAmount == 0 && saleInput.SellAll == false)
                throw new InvalidInputException(_path, "Sell()");

            _handleSaleService
                .Sell(saleInput.SessionId, saleInput.ShareAmount, saleInput.Symbol,
                    saleInput.SellAll);
        }

        [HttpPost]
        [Route("purchase")]
        public void Purchase(PurchaseInputModel purchaseInput)
        {
            if (purchaseInput.SessionId == null || purchaseInput.Symbol == null || purchaseInput.Amount == 0)
                throw new InvalidInputException(_path, "Purchase()");

            _handlePurchaseService
                .Purchase(purchaseInput.SessionId, purchaseInput.Amount, purchaseInput.Symbol);
        }
    }
}