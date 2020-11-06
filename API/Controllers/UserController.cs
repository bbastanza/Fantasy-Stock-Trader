// using API.Models;
// using API.Services;
using System.Collections.Generic;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private string _errorData;
        public UserController()
        {
            _errorData =  "new error";
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
        
        /// <summary>
        /// Post Request endpoint to add a new user to the database.
        /// </summary>
        /// <param name="newUser">Json object with a userName and password property</param>
        /// <returns></returns>

        [Route("adduser")]
        public IActionResult AddUser(UserModel newUser)
        {
         // returning message of it working or not working
         try
         {
             return Ok("New User Added " + JsonSerializer.Serialize(newUser));
         }
         catch
         {
             _errorData = "There was an error while adding a new user";
             return StatusCode(500, _errorData);
         }

        }
    }
}