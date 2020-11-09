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
        private readonly IJsonStockService _jsonStockService;

        private string _errorData = "new error";

        public StockDataController(IJsonStockService jsonStockService)
        {
            _jsonStockService = jsonStockService;
        }

        [Route("{stock}")]
        public IActionResult GetStockData(string stock)
        {
            try
            {
                return Ok(_jsonStockService.GetStockByName(stock));
            }
            catch
            {
                return StatusCode(500, _errorData);
            }
        }
    }
}