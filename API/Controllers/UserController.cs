using System.Collections.Generic;
using System.IO;
using System.Linq;
using API.Models;
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
        private readonly ILoginUser _loginUser;
        private readonly string _path;

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
            _path = Path.GetFullPath(ToString()!);
        }

        [HttpPost]
        [Route("getUserData")]
        public UserModel GetUser(SessionInputModel session)
        {
            if (session.SessionId == null)
                throw new InvalidInputException(_path, "GetUser()");
            
            return new UserModel(_getUserDataService.GetUserData(session.SessionId));
        }

        [HttpPost]
        [Route("transactions")]
        public IList<TransactionModel> GetUserTransactions(SessionInputModel session)
        {
            if (session.SessionId == null)
                throw new InvalidInputException(_path, "GetUserTransactions()");
            
            var transactions = _getUserDataService.GetUserTransactions(session.SessionId);
            return transactions.Select(transaction => new TransactionModel(transaction)).ToList();
        }

        [HttpPost]
        [Route("add")]
        public UserSessionModel AddUser(UserInputModel newUser)
        {
            if (newUser.UserName == null || newUser.Password == null || newUser.Email == null)
                throw new InvalidInputException(_path, "AddUser()");

            var userSession = _addUserService.AddUser(newUser.UserName, newUser.Password, newUser.Email);
            return new UserSessionModel(userSession.SessionId, userSession.ExpireDateTime);
        }

        [HttpPost]
        [Route("delete")]
        public string DeleteUser(UserInputModel userInput)
        {
            if (userInput.UserName == null || userInput.Password == null)
                throw new InvalidInputException(_path, "DeleteUser()");

            return _deleteUserService.DeleteUser(userInput.UserName, userInput.Password);
        }

        [HttpPost]
        [Route("login")]
        public UserSessionModel Login(UserInputModel userInput)
        {
            if (userInput.UserName == null || userInput.Password == null)
                throw new InvalidInputException(_path, "Login()");

            var userSession =  _loginUser.Login(userInput.UserName, userInput.Password);
            return new UserSessionModel(userSession.SessionId, userSession.ExpireDateTime);
        }
    }
}
