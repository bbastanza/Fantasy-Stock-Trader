using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockDataController : Controller
    {

        private string _errorData;

        public StockDataController()
        {
            _errorData = new string("new error");
        }

        [Route("{stock}+{amount}")]
        public IActionResult GetStockData(string stock, float amount)
        {
            try
            {
                var mockData = new MockStockData(stock,amount);
                return Ok(mockData);
            }
            catch
            {
                return StatusCode(500, _errorData);
            }
        }
    }
}