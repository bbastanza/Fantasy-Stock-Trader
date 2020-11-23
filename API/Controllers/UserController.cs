using API.Models;
using Core.Entities.Transactions.TransactionServices;
using Core.Entities.Users;
using Core.Entities.Users.Services;
using Microsoft.AspNetCore.Mvc;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IAddUserService _addUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserDataService _getUserDataService;
        private string _errorData;

        public UserController(IAddUserService addUserService, IDeleteUserService deleteUserService, IGetUserDataService getUserDataService)
        {
            _addUserService = addUserService;
            _deleteUserService = deleteUserService;
            _getUserDataService = getUserDataService;
            _errorData = "new error";
        }

        [HttpPost]
        [Route("getUser")]
        public IActionResult GetUser(UserInputModel userInputModel)
        {
            try
            {
                return Ok(JsonSerializer.Serialize(_getUserDataService.GetUserData(userInputModel.UserName, userInputModel.Password)));
            }
            catch
            {
                _errorData = "error while fetching user data";
                return StatusCode(500, _errorData);
            }
        }

        [HttpPost]
        [Route("addUser")]
        public IActionResult AddUser(UserInputModel newUser)
        {
            try
            {
                return Ok(JsonSerializer.Serialize(_addUserService.AddUser(newUser.UserName, newUser.Password, newUser.Email)));
            }
            catch
            {
                _errorData = "There was an error while adding a new user";
                return StatusCode(500, _errorData);
            }
        }

        [HttpDelete]
        [Route("deleteUser")]
        public IActionResult DeleteUser(UserInputModel user)
        {
            try
            {
                return Ok(_deleteUserService.DeleteUser(user.UserName, user.Password));
            }
            catch
            {
                _errorData = "There was an error while deleting user";
                return StatusCode(500, _errorData);
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LogInUser(UserInputModel user)
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
        public IActionResult LogOutUser(UserInputModel user)
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