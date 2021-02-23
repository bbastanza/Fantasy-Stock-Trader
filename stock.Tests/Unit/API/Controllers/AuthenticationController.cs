using API.Controllers;
using API.Models;
using API.Tests.TestUtilities;
using Core.Entities;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.Unit.API.Controllers
{
    public class AuthenticationControllerTests
    {
        private UserInputModel _emptyUserInputModel;
        private SessionInputModel _emptySessionInput;
        private AuthenticationController _sut;

        [SetUp]
        public void SetUp()
        {
            _emptyUserInputModel = new UserInputModel();
            _emptySessionInput = new SessionInputModel();
            
             var validUserInputModel = new UserInputModel()
            {
                UserName = TestData.CreateRandomString(),
                Password = TestData.CreateRandomString(),
                Email = TestData.CreateRandomString()
            };

            var loginUser = new Mock<ILoginService>();
            loginUser.Setup(x => x.Login(validUserInputModel.UserName, validUserInputModel.Password))
                .Returns(new UserSession());

            _sut = new AuthenticationController(loginUser.Object);
            
        }
        
        [Test]
        public void Login_InvalidInput_ThrowsInvalidInputException()
        {
            Assert.That(() => _sut.Login(_emptyUserInputModel),
                Throws.Exception.TypeOf<InvalidInputException>());
        }
        
        [Test]
        public void Logout_InvalidInput_ThrowsInvalidInputException()
        {
            Assert.That(() => _sut.Logout(_emptySessionInput),
                Throws.Exception.TypeOf<InvalidInputException>());
        }
    }
}