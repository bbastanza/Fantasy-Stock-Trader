using System.IO;
using Core.Entities;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockDataController : Controller
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly string _path;

        public StockDataController(IIexFetchService iexFetchService)
        {
            _iexFetchService = iexFetchService;
            _path = Path.GetFullPath(ToString()!);
        }

        [HttpGet]
        [Route("{stockSymbol}")]
        public IexStock GetStockData(string stockSymbol)
        {
            if (stockSymbol == null)
                throw new InvalidStockException(_path, "GetStockBySymbol()");

            return _iexFetchService.GetStockBySymbol(stockSymbol);
        }
    }
}