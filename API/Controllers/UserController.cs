using System;
using System.Collections.Generic;
using System.Linq;
using API.Models;
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
        private readonly ILoginUser _loginUser;

        public UserController(
            IAddUserService addUserService,
            IDeleteUserService deleteUserService,
            IGetUserDataService getUserDataService,
            ILoginUser loginUser
        )
        {
            _addUserService = addUserService;
            _deleteUserService = deleteUserService;
            _getUserDataService = getUserDataService;
            _loginUser = loginUser;
        }

        [HttpPost]
        [Route("get")]
        public UserModel GetUser(UserInputModel userInput)
        {
            var session = new UserSession();
            return new UserModel(_getUserDataService
                .GetUserData(userInput.UserName, userInput.Password));
        }

        [HttpPost]
        [Route("transactions")]
        public IList<TransactionModel> GetUserTransactions(UserInputModel userInput)
        {
            var transactions = _getUserDataService.GetUserTransactions(userInput.UserName);
            
            return transactions.Select(transaction => new TransactionModel(transaction)).ToList();
        }

        [HttpPost]
        [Route("add")]
        public UserModel AddUser(UserInputModel newUser)
        {
            return new UserModel(_addUserService
                .AddUser(newUser.UserName, newUser.Password, newUser.Email));
        }

        [HttpDelete]
        [Route("delete")]
        public string DeleteUser(UserInputModel user)
        {
            return _deleteUserService.DeleteUser(user.UserName, user.Password);
        }

        [HttpPost]
        [Route(("login"))]
        public UserSessionModel Login(UserInputModel userInput)
        {
            var userSession =  _loginUser.Login(userInput.UserName, userInput.Password);
            return new UserSessionModel(userSession.GuidString, userSession.ExpireDateTime);
        }
    }
}