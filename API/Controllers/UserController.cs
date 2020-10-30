using API.Models;
// using API.Services;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private string _errorData;

        public UserController()
        {
            _errorData = new string("new error");
        }

        [Route("user/{userName}+{password}")]
        public IActionResult GetMockData(string username, string password)
        {
            try
            {
                var mockData = new MockControllerData(username,password);
                return Ok(mockData);
            }
            catch
            {
                return StatusCode(500, _errorData);
            }
          
        }

        
    }
}