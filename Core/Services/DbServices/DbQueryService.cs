using System;
using System.Linq;
using Core.Entities;

namespace Core.Services.DbServices
{
    public interface IDbQueryService
    {
        string CheckExistingUser(string userName);
    }
    public class DbQueryService : IDbQueryService
    {
        private readonly INHibernateSessionService _nHibernateSessionService;

        public DbQueryService(INHibernateSessionService nHibernateSessionService)
        {
            _nHibernateSessionService = nHibernateSessionService;
        }

        public string CheckExistingUser(string userName)
        {
            var session = _nHibernateSessionService.GetSession();
            var currentUser = session.Query<User>()
                .Single(user => user.UserName == userName);
            Console.WriteLine(currentUser.UserName);
            return currentUser.ToString();
        }
    }
}