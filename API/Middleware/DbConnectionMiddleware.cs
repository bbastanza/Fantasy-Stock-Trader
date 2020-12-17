using System.Threading.Tasks;
using Core.Services.DbServices;
using Microsoft.AspNetCore.Http;
using NHibernate;

namespace API.MiddleWare
{
    public class DbConnectionMiddleware
    {
        private readonly RequestDelegate _next;

        public DbConnectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, INHibernateSession nHibernateSession)
        {
            var session = nHibernateSession.GetSession();
            
            using ITransaction transaction = session.BeginTransaction();
            await _next(context);

            try
            {
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                nHibernateSession.CloseSession();
            }
        }
    }
}