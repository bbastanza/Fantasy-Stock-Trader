using System;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockDataController : Controller
    {
        private readonly IIexFetchService _iexFetchService;
        private string _errorData = "There was an error fetching data from iex";

        public StockDataController(IIexFetchService iexFetchService)
        {
            _iexFetchService = iexFetchService;
        }

        [HttpGet]
        [Route("{stock}")]
        public IActionResult GetStockData(string stock)
        {
            try
            {
                return Ok(_iexFetchService.GetStockBySymbol(stock));
            }
            catch
            {
                return StatusCode(500, _errorData);
            }
        }
    }
}