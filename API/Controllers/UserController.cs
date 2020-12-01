using System;
using API.Models;
using API.OutputMappings;
using Core.Entities.Users.Services;
using Core.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IAddUserService _addUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserDataService _getUserDataService;
        private readonly IUserOutputMap _userOutputMap;

        public UserController(
            IAddUserService addUserService, 
            IDeleteUserService deleteUserService, 
            IGetUserDataService getUserDataService, 
            IUserOutputMap userOutputMap)
        {
            _addUserService = addUserService;
            _deleteUserService = deleteUserService;
            _getUserDataService = getUserDataService;
            _userOutputMap = userOutputMap;
        }

        [HttpPost]
        [Route("get")]
        public IActionResult GetUser(UserInputModel userInput)
        {
            try
            {
                var userOutput =
                    _userOutputMap.MapUserOutput(
                        _getUserDataService.GetUserData(userInput.UserName, userInput.Password));
                return Ok(userOutput);
            }            
            catch(Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n \nStackTrace: {ex.StackTrace}");
                return StatusCode(500,  ex.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddUser(UserInputModel newUser)
        {
            try
            {
                var userOutput =
                    _userOutputMap.MapUserOutput(_addUserService.AddUser(newUser.UserName, newUser.Password,
                        newUser.Email));
                return Ok(userOutput);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n \nStackTrace: {ex.StackTrace}");
                return StatusCode(500,  ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(UserInputModel user)
        {
            try
            {
                return Ok(_deleteUserService.DeleteUser(user.UserName, user.Password));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n \nStackTrace: {ex.StackTrace}");
                return StatusCode(500,  ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LogInUser(UserInputModel user)
        {
            try
            {
                // check to see if user is in the database ()=> if true check to see if user.password == databaseuser.password
                return Ok(user.UserName + " is now logged in");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n \nStackTrace: {ex.StackTrace}");
                return StatusCode(500,  ex.Message);
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
            catch(Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n \nStackTrace: {ex.StackTrace}");
                return StatusCode(500,  ex.Message);
            }
        }
    }
}