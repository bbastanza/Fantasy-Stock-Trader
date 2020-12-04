using System.IO;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.TransactionServices;
using Infrastructure.Exceptions;

namespace Core.Services.UserServices
{
    public interface IGetUserDataService
    {
        User GetUserData(string userName, string password);
    }

    public class GetUserDataService : IGetUserDataService
    {
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;
        private readonly IDbQueryService _dbQueryService;

        public GetUserDataService(ISetAllocatedFundsService setAllocatedFundsService,
            IStockListService stockListService, IDbQueryService dbQueryService)
        {
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
            _dbQueryService = dbQueryService;
        }

        public User GetUserData(string userName, string password)
        {
            if (userName == null || password == null)
                throw new InvalidInputException(Path.GetFullPath(ToString()), "GetUserData()");
            
            var user = _dbQueryService.GetUserFromDb(userName, password);
            
            user.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(_stockListService.GetStockModelList(user), user.Holdings);

            return user;
        }
    }
}