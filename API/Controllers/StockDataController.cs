using System;
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

        [HttpGet]
        [Route("getAvailable")]
        public IActionResult GetAvailableStocks()
        {
            try
            {
                return Ok("got available stocks");
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
    }
}