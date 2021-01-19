using System;
using System.Reflection;
using Core.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface INHibernateSessionFactory
    {
        ISession OpenSession();
    }

    public class NHibernateSessionFactory : INHibernateSessionFactory
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateSessionFactory(IConfiguration configuration)
        {
            _sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82
                    .ConnectionString(config => config
                        .Host(configuration["sessionFactory:Host"])
                        .Username(configuration["sessionFactory:Username"])
                        .Password(configuration["sessionFactory:Password"])
                        .Database(configuration["sessionFactory:Database"])
                        .Port(Convert.ToInt32(configuration["sessionFactory:Port"]))
                    ))
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(Assembly.GetAssembly(typeof(HoldingMap))))
                .BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
