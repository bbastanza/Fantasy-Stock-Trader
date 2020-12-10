using System;
using System.Threading.Tasks;
using API.Models;
using Core.Services.DbServices;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using NHibernate;
using ISession = NHibernate.ISession;

namespace API.MiddleWare
{
    public class DbConnectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly INHibernateSessionService _nHibernateSessionService;
        private readonly ISession _session;

        public DbConnectionMiddleware(
            RequestDelegate next,
            INHibernateSessionService nHibernateSessionService)
        {
            _next = next;
            _nHibernateSessionService = nHibernateSessionService;
            _session = nHibernateSessionService.GetSession();
        }

        public async Task Invoke(HttpContext context)
        {
            using ITransaction transaction = _session.BeginTransaction();
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
                _nHibernateSessionService.CloseSession();
            }
        }
    }
}