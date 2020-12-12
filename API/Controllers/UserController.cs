using System.Collections.Generic;
using API.Models;
using API.OutputMappings;
using Core.Entities;
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
        public UserModel GetUser(UserInputModel userInput)
        {
                return _userOutputMap.MapUserOutput(_getUserDataService
                    .GetUserData(userInput.UserName, userInput.Password));
        }

        [HttpPost]
        [Route("transactions")]
        public IList<TransactionModel> GetUserTransactions(UserInputModel userInput)
        {
            var transactions = _getUserDataService.GetUserTransactions(userInput.UserName);
            var transactionModels = new List<TransactionModel>();
            foreach (var transaction in transactions)
            {
               transactionModels.Add(new TransactionModel(transaction)); 
            }

            return transactionModels;
        }
        
        [HttpPost]
        [Route("add")]
        public UserModel AddUser(UserInputModel newUser)
        {
            return _userOutputMap.MapUserOutput(_addUserService
                .AddUser(newUser.UserName, newUser.Password, newUser.Email));
        }

        [HttpDelete]
        [Route("delete")]
        public string DeleteUser(UserInputModel user)
        {
                return _deleteUserService.DeleteUser(user.UserName, user.Password);
        }
    }
}