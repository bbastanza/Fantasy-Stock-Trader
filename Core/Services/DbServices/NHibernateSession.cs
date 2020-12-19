using System.Reflection;
using Core.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface INHibernateSession
    {
        ISession GetSession();
        void CloseSession();
    }

    public class NHibernateSession : INHibernateSession
    {
        private static readonly ISessionFactory _sessionFactory;
        private ISession _session;

        static NHibernateSession()
        {
            _sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82
                    .ConnectionString(config => config
                        .Host("localhost")
                        .Username("stanzu10")
                        .Password("Emma1234")
                        .Database("stock_db")
                        .Port(5432)
                    )) 
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(Assembly.GetAssembly(typeof(HoldingMap))))
                .BuildSessionFactory();
        }

        public ISession GetSession()
        {
            return _session ?? (_session = _sessionFactory.OpenSession());
        }

        public void CloseSession()
        {
            _sessionFactory.Close();
        }

        private void ConfigureSessionFactory(IConfiguration configuration)
        {
            
        }
    }
}

