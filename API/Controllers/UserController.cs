using API.Models;
// using API.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private string _errorData;
        private List<UserData> Users = new List<UserData>();

        public UserController()
        {
            var brian = new UserData(){UserName = "Brian",Password = "password"};
            Users.Add(brian);
        }

        [Route("/getusers")]
        public IActionResult GetMockData()
        {
            try
            {
                return Ok(Users);
            }
            catch
            {
                return StatusCode(500, _errorData);
            }
        }

        [Route("/adduser")]
        public void AddUser(UserData value)
        {
            Users.Add(value);
        }
    }
}