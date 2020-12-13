using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Core.Services.IexServices;
using Infrastructure.Exceptions;
using NHibernate;
using NHibernate.Linq;

namespace Core.Services.TransactionServices
{
    public interface IHandleSaleService
    {
        Transaction SellTransaction(double amount, string userName, string symbol, bool sellAll = false);
    }

    public class HandleSaleService : IHandleSaleService
    {
        private readonly IIexFetchService _iexFetchService;
        private readonly ISetAllocatedFundsService _setAllocatedFundsService;
        private readonly string _path;
        private readonly ISession _session;

        public HandleSaleService(
            IIexFetchService iexFetchService,
            ISetAllocatedFundsService setAllocatedFundsService,
            INHibernateSessionService nHibernateSessionService)
        {
            _iexFetchService = iexFetchService;
            _setAllocatedFundsService = setAllocatedFundsService;
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public Transaction SellTransaction(double amount, string userName, string symbol, bool sellAll = false)
        {
            return new Transaction();

        }


    }
}