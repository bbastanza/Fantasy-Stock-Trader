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
    [TestFixture]
    public class UserControllerTests
    {
        private UserInputModel _emptyUserInputModel;
        private SessionInputModel _emptySessionInput;
        private UsersController _sut;

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

             var user = new User(
                 TestData.CreateRandomString(),
                 TestData.CreateRandomString(),
                 TestData.CreateRandomString());
             
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

            _sut = new UsersController(addUserService.Object, deleteUserService.Object, getUserDataService.Object);
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