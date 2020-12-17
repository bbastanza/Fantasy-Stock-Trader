using System;
using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.UserServices
{
    public interface ICheckExpiration
    {
        User CheckUserSession(string sessionId);
    }
    
    public class CheckExpiration : ICheckExpiration
    {
        private readonly ISession _session;
        private readonly string _path;

        public CheckExpiration(INHibernateSession nHibernateSession)
        {
            _session = nHibernateSession.GetSession();
            _path = Path.GetFullPath(ToString());
        }
        
        public User CheckUserSession(string sessionId)
        {
            var userSession = _session.Query<UserSession>()
                .FirstOrDefault(x => x.SessionId == sessionId);
            
            if (userSession == null)
                throw new NonExistingSessionException(_path, "CheckUserSession()");

            if (userSession.ExpireDateTime < DateTime.Now)
                throw new ExpiredSessionException(_path, "CheckUserSession()");

            return userSession.User;
        }
    }
}