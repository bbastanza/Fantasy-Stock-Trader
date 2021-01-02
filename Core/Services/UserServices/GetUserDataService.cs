using System.Collections.Generic;
using System.IO;
using Core.Entities;
using Core.Services.TransactionServices;
using Infrastructure.Exceptions;

namespace Core.Services.UserServices
{
    public interface IGetUserDataService
    {
        User GetUserData(string sessionId);
        IList<Transaction> GetUserTransactions(string sessionId);
    }

    public class GetUserDataService : IGetUserDataService
    {
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly ICheckExpiration _checkExpiration;
        private readonly string _path;

        public GetUserDataService(
            ISetAllocatedFundsService setAllocatedFundsService,
            ICheckExpiration checkExpiration)
        {
            _setAllocatedFundsService = setAllocatedFundsService;
            _checkExpiration = checkExpiration;
            _path = Path.GetFullPath(ToString());
        }

        public User GetUserData(string sessionId)
        {
            var user = _checkExpiration.CheckUserSession(sessionId);
 
            if (user == null)
                throw new NonExistingUserException(_path, "GetUserData()");
            
            user.AllocatedFunds = _setAllocatedFundsService.SetAllocatedFunds(user);

            return user;
        }

        public IList<Transaction> GetUserTransactions(string sessionId)
        {
            var user = _checkExpiration.CheckUserSession(sessionId);

            return user.Transactions;
        }
    }
}