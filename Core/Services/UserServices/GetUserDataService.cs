using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.TransactionServices;
using Infrastructure.Exceptions;
using NHibernate.Linq;

namespace Core.Services.UserServices
{
    public interface IGetUserDataService
    {
        User GetUserData(string userName, string password);
        IList<Transaction> GetUserTransactions(string userName);
    }

    public class GetUserDataService : IGetUserDataService
    {
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly IStockListService _stockListService;
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly string _path;

        public GetUserDataService(
            ISetAllocatedFundsService setAllocatedFundsService,
            IStockListService stockListService,
            INHibernateSessionService nHibernateSessionService)
        {
            _setAllocatedFundsService = setAllocatedFundsService;
            _stockListService = stockListService;
            _nHibernateSessionService = nHibernateSessionService;
            _path = Path.GetFullPath(ToString());
        }

        public User GetUserData(string userName, string password)
        {
            var session = _nHibernateSessionService.GetSession();
            if (userName == null || password == null)
                throw new InvalidInputException(_path, "GetUserData()");

            // if (!_dbQueryService.ValidateUser(userName, password))
            //     throw new UserValidationException(_path, "GetUserData()");
            
            var user =  session.Query<User>()
                .SingleOrDefault(x => x.UserName == userName);

            if (user != null)
            {
                user.AllocatedFunds =
                    _setAllocatedFundsService.SetAllocatedFunds(user);
            }
            
            return user;
        }

        public IList<Transaction> GetUserTransactions(string userName)
        { 
            var session = _nHibernateSessionService.GetSession();
            return session.Query<User>()
                .SingleOrDefault(x => x.UserName == userName)
                ?.Transactions;

        }
    }
}