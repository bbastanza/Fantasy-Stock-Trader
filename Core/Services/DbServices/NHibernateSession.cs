using System.Reflection;
using Core.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface INHibernateSessionService
    {
        ISession GetSession();
        void CloseSession();
    }

    public class NHibernateSessionService : INHibernateSessionService
    {
        private static readonly ISessionFactory _sessionFactory;

        static NHibernateSessionService()
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
                .Mappings(m =>
                    m.FluentMappings.AddFromAssembly(Assembly
                                .GetAssembly(typeof(HoldingMap))))
                .BuildSessionFactory();
        }

        public ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }

        public void CloseSession()
        {
            _sessionFactory.Close();
        }
    }
}

// User ID=postgres;Password=password;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;}
