using System;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockDataController : Controller
    {
        private readonly IJsonStockService _jsonStockService;

        private string _errorData;

        public StockDataController(IJsonStockService jsonStockService)
        {
            _jsonStockService = jsonStockService;
            _errorData = new string("new error");
        }

        [Route("{stock}")]
        public IActionResult GetStockData(string stock)
        {
            try
            {
                var response = _jsonStockService.GetStockByName(stock);
                // Console.WriteLine(response);
                return Ok(response);
            }
            catch
            {
                return StatusCode(500, _errorData);
            }
        }
    }
}