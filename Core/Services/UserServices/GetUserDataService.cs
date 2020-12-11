using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.TransactionServices;
using Infrastructure.Exceptions;

namespace Core.Services.UserServices
{
    public interface IGetUserDataService
    {
        User GetUserData(string userName, string password);
        List<Transaction> GetUserTransactions(string userName);
    }

    public class GetUserDataService : IGetUserDataService
    {
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;
        private readonly IDbQueryService _dbQueryService;
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;

        public GetUserDataService(
            ISetAllocatedFundsService setAllocatedFundsService,
            IStockListService stockListService,
            IDbQueryService dbQueryService,
            INHibernateSessionService nHibernateSessionService)
        {
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
            _dbQueryService = dbQueryService;
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }

        public User GetUserData(string userName, string password)
        {
            if (userName == null || password == null)
                throw new InvalidInputException(_path, "GetUserData()");

            if (!_dbQueryService.ValidateUser(userName, password))
                throw new UserValidationException(_path, "GetUserData()");
            
            var user = _dbQueryService.GetUserFromDb(userName);

            user.Holdings = _dbQueryService.GetUserHoldings(user.Id);

            user.AllocatedFunds =
                _setAllocatedFundsService.SetAllocatedFunds(_stockListService.GetStockModelList(user), user.Holdings);

            return user;
        }

        public List<Transaction> GetUserTransactions(string userName)
        {
            var session = _nHibernateSessionService.GetSession();
            return session.Query<User>()
                .Where(x => x.UserName = userName)
                .

        }
    }
}