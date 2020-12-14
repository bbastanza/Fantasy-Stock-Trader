using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.TransactionServices;
using Infrastructure.Exceptions;
using NHibernate;

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
        private readonly string _path;
        private readonly ISession _session;

        public GetUserDataService(
            ISetAllocatedFundsService setAllocatedFundsService,
            INHibernateSession inHibernateSession)
        {
            _setAllocatedFundsService = setAllocatedFundsService;
            _session = inHibernateSession.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public User GetUserData(string userName, string password)
        {
            if (userName == null || password == null)
                throw new InvalidInputException(_path, "GetUserData()");

            var user = _session.Query<User>().FirstOrDefault(x => x.UserName == userName);
            
            if (user == null)
                throw new NonExistingUserException(_path, "GetUserData()");
            
            user.AllocatedFunds = _setAllocatedFundsService.SetAllocatedFunds(user);

            return user;
        }

        public IList<Transaction> GetUserTransactions(string userName)
        {
            return _session.Query<User>().FirstOrDefault(x => x.UserName == userName)
                ?.Transactions;
        }
    }
}