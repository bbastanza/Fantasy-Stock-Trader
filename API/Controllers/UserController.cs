using System;
using API.Models;
using API.OutputMappings;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
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
            catch (DreamTraderException ex)
            {
                Console.WriteLine($"{ex.GetType()}\n{ex.Message}\nPath {ex.Path}.{ex.Method}");
                return StatusCode(409, new ExceptionModel(ex));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n \nStackTrace: {ex.StackTrace}");
                return StatusCode(500,  ex);
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
            catch (DreamTraderException ex)
            {
                Console.WriteLine($"Message {ex.GetType()}\n{ex.Message}\nPath {ex.Path}{ex.Method}");
                return StatusCode(409, new ExceptionModel(ex));
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
            catch (DreamTraderException ex)
            {
                Console.WriteLine($"{ex.GetType()}\n{ex.Message}\nPath {ex.Path}.{ex.Method}");
                return StatusCode(409, new ExceptionModel(ex));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n \nStackTrace: {ex.StackTrace}");
                return StatusCode(500,  ex.Message);
            }
        }
    }
}