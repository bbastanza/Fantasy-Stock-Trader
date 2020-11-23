// using System.Net;
// using System.Net.Http;
// using NHibernate;
// using NHibernate.Cfg;
//
// namespace Core.NHibernate
// {
//     public class NHibernateSession
//     {
//         public static ISession OpenSession()
//         {
//             var configuration = new Configuration();
//             var configurationPath = HttpContext.Current.Server.MapPath(@"~/Core/Models/hibernate.cfg.xml");
//             configuration.Configure(configurationPath);
//             var bookConfigurationFile = HttpContext.Current.Server.MapPath(@"~/Core/Mappings/User.hbm.xml");
//             configuration.AddFile(bookConfigurationFile);
//             ISessionFactory sessionFactory = configuration.BuildSessionFactory();
//             return sessionFactory.OpenSession();
//         }
//     }
// }