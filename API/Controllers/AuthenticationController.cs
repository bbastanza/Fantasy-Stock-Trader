using System.IO;
using API.Models;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly string _path;

        public AuthenticationController(
            ILoginService loginService
        )
        {
            _loginService = loginService;
            _path = Path.GetFullPath(ToString()!);
        }

        [HttpPost]
        [Route("login")]
        public UserSessionModel Login(UserInputModel userInput)
        {
            if (userInput.UserName == null || userInput.Password == null)
                throw new InvalidInputException(_path, "Login()");

            var userSession =  _loginService.Login(userInput.UserName, userInput.Password);
            
            return new UserSessionModel(userSession.SessionId, userSession.ExpireDateTime);
        }
        
        [HttpPost]
        [Route("logout")]
        public void Logout(SessionInputModel session)
        {
            if (session.SessionId == null)
                throw new InvalidInputException(_path, "Logout()");

            _loginService.Logout(session.SessionId);
        }
    }
}