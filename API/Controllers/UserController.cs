// using API.Models;
// using API.Services;
using System.Collections.Generic;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private string _errorData = "new error";
        public UserController()
        {
            
        }

        [Route("getuser/{user}")]
        public IActionResult GetUser(string user)
        {
            try
            {
                return Ok(new UserModel());
            }
            catch
            {
                return StatusCode(500, _errorData);
            }
        }

        [Route("/adduser")]
        public void AddUser(string value)
        {
         
        }
    }
}