using NHibernate;

namespace Core.Services.DbServices
{
    public interface IUnitOfWork
    {
        ISession GetSession();
        void CloseSession();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;

        public UnitOfWork(INHibernateSessionFactory nHibernateSessionFactory)
        {
            _session = nHibernateSessionFactory.OpenSession();
        }

        public ISession GetSession()
        {
            return _session;
        }

        public void CloseSession()
        {
            _session.Close();
        }
    }
}