using System;
using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface IDbUpdateService
    {
        void Update(EntityBase entity);
    }
    public class DbUpdateService : IDbUpdateService
    {
        private readonly string _path;
        private readonly ISession _session;

        public DbUpdateService(INHibernateSessionService nHibernateSessionService)
        {
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public async void Update(EntityBase entity)
        {
            try
            {
                    await _session.UpdateAsync(entity);
                    await _session.FlushAsync();
            }
            catch
            {
                throw new DbInteractionException(_path, "UpdateBalance()");
            }
        }
    }
}