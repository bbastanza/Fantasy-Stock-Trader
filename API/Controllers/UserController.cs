// using API.Models;
// using API.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        
        public UserController()
        {
            
        }

        [Route("/getusers")]
        public IActionResult GetMockData()
        {
            try
            {
                return Ok(new object());
            }
            catch
            {
                return StatusCode(500, new object());
            }
        }

        [Route("/adduser")]
        public void AddUser(string value)
        {
         
        }
    }
}