using System.Linq;
using Core.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Core.Services.DbServices
{
    public interface IQueryDb
    {
        User GetUser(string userName);
        UserSession GetSession(string sessionId);
        void DeleteSession(string sessionId);
        void DeleteUser(string userName);
        void SaveToDb(EntityBase entity);
    }

    public class QueryDb : IQueryDb
    {
        private readonly ISession _session;

        public QueryDb(INHibernateSession nHibernateSession)
        {
            _session = nHibernateSession.GetSession();
        }

        public User GetUser(string userName)
        {
            return _session.Query<User>().FirstOrDefault(x => x.UserName == userName);
        }

        public UserSession GetSession(string sessionId)
        {
            return _session.Query<UserSession>().FirstOrDefault(x => x.SessionId == sessionId);
        }

        public void DeleteUser(string userName)
        {
            _session.Query<User>()
                .Where(user => user.UserName == userName).Delete();
        }

        public void DeleteSession(string sessionId)
        {
            _session.Query<UserSession>().Where(x => x.SessionId == sessionId).Delete();
        }

        public void SaveToDb(EntityBase entity)
        {
            _session.Save(entity);
        }
    }
}