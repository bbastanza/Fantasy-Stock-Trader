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
        
        
        /// <summary>
        /// Get user information from the database minus the password
        /// </summary>
        /// <param name="user">User to get data back for</param>
        /// <returns>IActionResult with UserModel if successful, 500 response if unsuccessful</returns>
        [Route("getUser/{userName}")]
        public IActionResult GetUser(string userName)
        {
            try
            {
                // check to see ()=> if userName exists in the database as a UserModel.userName ()=> if not return StatusCode(500, "error user does not exist")
                // else return UserModel minus the password
                return Ok(JsonSerializer.Serialize(new UserModel(){UserName = userName}));
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

        [Route("addUser")]
        public IActionResult AddUser(UserModel newUser)
        {
            try
            {
                // check to see ()=> if newUser.userName exists in the database => if true return "error user already in database"
                // else ()=> add newUser(userModel to database) ()=> return valid response
                return Ok("New User Added " + JsonSerializer.Serialize(newUser));
            }
            catch
            {
                _errorData = "There was an error while adding a new user";
                return StatusCode(500, _errorData);
            }

        }
        
        /// <summary>
        /// Post request to delete a user from the database
        /// </summary>
        /// <param name="user">Json object including the userName and password to be deleted</param>
        /// <returns></returns>
        
        [Route("deleteUser")]
        public IActionResult DeleteUser(UserModel user)
        {
            try
            {
                // check to see ()=> user.userName != in the database => if true return StatusCode(500, "error user does not exist")
                // else ()=> if password == UserModel.password
                //     delete user from the database ()=> return valid response
                return Ok("deleted user " + JsonSerializer.Serialize(user));
            }
            catch
            {
                _errorData = "There was an error while adding deleting user";
                return StatusCode(500, _errorData);
            }

        }
    }
}