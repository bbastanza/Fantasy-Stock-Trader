using Core.Models;
using Infrastructure;
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
        /// GET request user information from the database minus the password
        /// </summary>
        /// <param name="userName">string userName to get data back for</param>
        /// <returns>IActionResult with UserModel if successful, 500 response if unsuccessful</returns>
        [Route("getUser/{userName}")]
        public IActionResult GetUser(string userName)
        {
            try
            {
                // check to see ()=> if userName exists in the database as a UserModel.userName ()=> if not return StatusCode(500, "error user does not exist")
                // else return UserModel minus the password
                var currentUser = new UserModel(userName, "Password");
                var userInfrastructure = new TransactionInfrastructure(currentUser);
                currentUser.SetAllocatedDollars(userInfrastructure.SymbolList);
                return Ok(JsonSerializer.Serialize(currentUser));
            }
            catch
            {
                _errorData = "error while fetching user data";
                return StatusCode(500, _errorData);
            }
        }
        
        /// <summary>
        /// POST Request endpoint to add a new user to the database.
        /// </summary>
        /// <param name="newUser">Json object with a userName and password property</param>
        /// <returns>IActionResult with status message</returns>
        [Route("addUser")]
        public IActionResult AddUser(UserModel newUser)
        {
            try
            {
                // check to see ()=> if newUser.userName exists in the database => if true return "error user already in database"
                // else ()=> add newUser(userModel to database) ()=> return valid response
                return Ok("New User Added " + JsonSerializer.Serialize(new UserModel(newUser.UserName,newUser.Password)));
            }
            catch
            {
                _errorData = "There was an error while adding a new user";
                return StatusCode(500, _errorData);
            }
        }
        
        /// <summary>
        /// DELETE request to delete a user from the database
        /// </summary>
        /// <param name="user">Json object including the userName and password to be deleted</param>
        /// <returns>IActionResult with status message</returns>
        [Route("deleteUser")]
        public IActionResult DeleteUser(UserModel user)
        {
            try
            {
                // check to see ()=> user.userName != in the database => if true return StatusCode(500, "error user does not exist")
                // else ()=> if password == UserModel.password
                //     delete user from the database ()=> return valid response
                return Ok("deleted user " + JsonSerializer.Serialize(new UserModel(user.UserName,user.Password)));
            }
            catch
            {
                _errorData = "There was an error while deleting user";
                return StatusCode(500, _errorData);
            }
        }
        
        /// <summary>
        /// POST request endpoint to log in a user
        /// </summary>
        /// <param name="user">Json object with user model to log in</param>
        /// <returns>IActionResult with status message</returns>
        [Route("login")]
        public IActionResult LogInUser(UserModel user)
        {
            try
            {
                // check to see if user is in the database ()=> if true check to see if user.password == databaseuser.password
                return Ok(JsonSerializer.Serialize(user.UserName) + " is now logged in");
            }
            catch
            {
                _errorData = "Login Error";
                return StatusCode(500, _errorData);
            }
        }
        
        /// <summary>
        /// POST request endpoint to logout a user
        /// </summary>
        /// <param name="userName">string userName to log out</param>
        /// <returns>IActionResult with status message</returns>
        [Route("logout/{userName}")]
        public IActionResult LogOutUser(string userName)
        {
            try
            {
                // check to see if user is in the database ()=> logout user
                return Ok(userName + " has logged out");
            }
            catch
            {
                _errorData = "Log Out Error";
                return StatusCode(500, _errorData);
            }
        }
    }
}
