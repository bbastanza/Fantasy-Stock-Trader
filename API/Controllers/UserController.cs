using Core.Models;
using Core.Services;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly ITransactionInfrastructure _transactionInfrastructure;
        private string _errorData;

        public UserController(IIexFetchService iexFetchService, ITransactionInfrastructure transactionInfrastructure)
        {
            _iexFetchService = iexFetchService;
            _transactionInfrastructure = transactionInfrastructure;
            _errorData = "new error";
        }

        [HttpGet]
        [Route("getUser/{userName}")]
        public IActionResult GetUser(string userName)
        {
            try
            {
                // check to see ()=> if userName exists in the database as a UserModel.userName ()=> if not return StatusCode(500, "error user does not exist")
                // else return UserModel minus the password
                var currentUser = new UserModel(userName, "Password");
                currentUser.SetAllocatedDollars(_transactionInfrastructure.GetStockModelList(currentUser));
                return Ok(JsonSerializer.Serialize(currentUser));
            }
            catch
            {
                _errorData = "error while fetching user data";
                return StatusCode(500, _errorData);
            }
        }

        [HttpPost]
        [Route("addUser")]
        public IActionResult AddUser(UserModel newUser)
        {
            try
            {
                // check to see ()=> if newUser.userName exists in the database => if true return "error user already in database"
                // else ()=> add newUser(userModel to database) ()=> return valid response
                return Ok("New User Added " +
                          JsonSerializer.Serialize(new UserModel(newUser.UserName, newUser.Password)));
            }
            catch
            {
                _errorData = "There was an error while adding a new user";
                return StatusCode(500, _errorData);
            }
        }

        [HttpDelete]
        [Route("deleteUser")]
        public IActionResult DeleteUser(UserModel user)
        {
            try
            {
                // check to see ()=> user.userName != in the database => if true return StatusCode(500, "error user does not exist")
                // else ()=> if password == UserModel.password
                //     delete user from the database ()=> return valid response
                return Ok("deleted user " + JsonSerializer.Serialize(new UserModel(user.UserName, user.Password)));
            }
            catch
            {
                _errorData = "There was an error while deleting user";
                return StatusCode(500, _errorData);
            }
        }

        [HttpPost]
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

        [HttpPost]
        [Route("logout")]
        public IActionResult LogOutUser(UserModel user)
        {
            try
            {
                // check to see if user is in the database ()=> logout user
                return Ok(user.UserName + " has logged out");
            }
            catch
            {
                _errorData = "Log Out Error";
                return StatusCode(500, _errorData);
            }
        }
    }
}