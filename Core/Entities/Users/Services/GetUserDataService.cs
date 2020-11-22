using Core.Entities.Transactions.TransactionServices;

namespace Core.Entities.Users.Services
{
    public interface IGetUserDataService
    {
        User GetUserData(string userName, string password);
    }
    public class GetUserDataService : IGetUserDataService
    {
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;

        public GetUserDataService(ISetAllocatedFundsService setAllocatedFundsService, IStockListService stockListService)
        {
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
        }
        public User GetUserData(string userName, string password)
        {
            // _checkUserService.ValidateUser(userName, password)
            // if true _getUserDataService.GetUserByUsername(username)
            // this code is temporary
            var user = new User(userName, password, "some@email.com");
            user.AllocatedFunds = _setAllocatedFundsService.SetAllocatedFunds(_stockListService.GetStockModelList(user), user.Holdings);
            

            return user;
        }
        
    }
}