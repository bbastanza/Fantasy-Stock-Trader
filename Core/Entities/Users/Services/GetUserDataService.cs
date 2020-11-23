using System.IO;
using Core.Entities.Transactions.TransactionServices;

namespace Core.Entities.Users.Services
{
    public interface IGetUserDataService
    {
        UserEntity GetUserData(string userName, string password);
    }

    public class GetUserDataService : IGetUserDataService
    {
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;

        public GetUserDataService(ISetAllocatedFundsService setAllocatedFundsService,
            IStockListService stockListService)
        {
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
        }

        public UserEntity GetUserData(string userName, string password)
        {
            if (userName == null || password == null)
                throw new InvalidDataException(
                    $"You have provided incomplete data\nUserName: {userName}\nPassword: {password}");
            // _checkUserService.ValidateUser(userName, password)
            // if true _getUserDataService.GetUserByUsername(username)
            // this code is temporary
            var user = new UserEntity(userName, password, "some@email.com");
            //this code will stay
            user.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(_stockListService.GetStockModelList(user), user.Holdings);


            return user;
        }
    }
}