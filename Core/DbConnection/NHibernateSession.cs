using Core.Entities.Users;
using Core.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Core.DbConnection
{
    public interface INHibernateSessionService
    {
        ISessionFactory ConfigureSession();
    }
    public class InHibernateSessionService : INHibernateSessionService
    {
        public ISessionFactory ConfigureSession()
        {
            return Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard)
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssemblyOf<HoldingMap>()
                        .AddFromAssemblyOf<UserMap>()
                        .AddFromAssemblyOf<TransactionMap>())
                .BuildSessionFactory();
        }
    }
}