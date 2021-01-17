using API.Controllers;
using API.Models;
using Core.Entities;
using Core.Services.UserServices;
using Infrastructure.Exceptions;
using Moq;
using NUnit.Framework;

namespace API.Tests.UnitTests.APITests.ControllerTests
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserInputModel _emptyUserInputModel;
        private SessionInputModel _emptySessionInput;
        private UserController _sut;

        [SetUp]
        public void SetUp()
        {
            _emptyUserInputModel = new UserInputModel();
            _emptySessionInput = new SessionInputModel();
            
             var validUserInputModel = new UserInputModel()
            {
                UserName = "username",
                Password = "password",
                Email = "email"
            };

            var user = new User("username", "password", "email");
            var getUserDataService = new Mock<IGetUserDataService>();
            getUserDataService.Setup(x => x.GetUserData("1"))
                .Returns(user);
            
            var addUserService = new Mock<IAddUserService>();
            addUserService.Setup(x => x.AddUser(validUserInputModel.UserName, validUserInputModel.Password,
                    validUserInputModel.Email))
                .Returns(new UserSession());

            var deleteUserService = new Mock<IDeleteUserService>();
            deleteUserService.Setup(x => x.DeleteUser(validUserInputModel.UserName, validUserInputModel.Password))
                .Returns("deleted");

            var loginUser = new Mock<ILoginService>();
            loginUser.Setup(x => x.Login(validUserInputModel.UserName, validUserInputModel.Password))
                .Returns(new UserSession());

            _sut = new UserController(addUserService.Object, deleteUserService.Object, getUserDataService.Object, loginUser.Object);
        }

        [Test]
        public void AddUser_InvalidInput_ThrowsInvalidInputException()
        {
            Assert.That(() => _sut.AddUser(_emptyUserInputModel),
                Throws.Exception.TypeOf<InvalidInputException>());
        }

        [Test]
        public void DeleteUser_InvalidInput_ThrowsInvalidInputException()
        {
            Assert.That(() => _sut.DeleteUser(_emptyUserInputModel),
                Throws.Exception.TypeOf<InvalidInputException>());
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
        
        [Test]
        public void GetUser_InvalidInput_ThrowsInvalidInputException()
        {
            Assert.That(() => _sut.GetUser(_emptySessionInput),
                Throws.Exception.TypeOf<InvalidInputException>());
        }
        
        [Test]
        public void GetUserTransactions_InvalidInput_ThrowsInvalidInputException()
        {
            Assert.That(() => _sut.GetUserTransactions(_emptySessionInput),
                Throws.Exception.TypeOf<InvalidInputException>());
        }
    }
}