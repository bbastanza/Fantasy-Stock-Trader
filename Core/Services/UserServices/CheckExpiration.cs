using System;
using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;
using NHibernate;
using NHibernate.Linq;

namespace Core.Services.UserServices
{
    public interface ICheckExpiration
    {
        User CheckUserSession(string sessionId);
    }

    public class CheckExpiration : ICheckExpiration
    {
        private readonly IQueryDb _queryDb;
        private readonly string _path;

        public CheckExpiration(INHibernateSession nHibernateSession, IQueryDb queryDb)
        {
            _queryDb = queryDb;
            _path = Path.GetFullPath(ToString());
        }

        public User CheckUserSession(string sessionId)
        {
            var userSession = _queryDb.GetSession(sessionId);

            if (userSession == null)
                throw new NonExistingSessionException(_path, "CheckUserSession()");

            if (userSession.ExpireDateTime >= DateTime.Now) 
                return userSession.User;

            _queryDb.DeleteSession(sessionId);
            
            throw new ExpiredSessionException(_path, "CheckUserSession()");
        }
    }
}