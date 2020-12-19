using Core.Entities;
using Core.Services.IexServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockDataController : Controller
    {
        private readonly IIexFetchService _iexFetchService;

        public StockDataController(IIexFetchService iexFetchService)
        {
            _iexFetchService = iexFetchService;
        }

        [HttpGet]
        [Route("{stock}")]
        public IexStock GetStockData(string stock) => _iexFetchService.GetStockBySymbol(stock);
    }
}
