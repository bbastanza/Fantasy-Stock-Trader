using System.IO;
using Core.Entities;
using Infrastructure.Exceptions;
using NHibernate;

namespace Core.Services.DbServices
{
    public interface IDbAddService
    {
        void Add(EntityBase entity);
    }
    public class DbAddService : IDbAddService
    {
        private readonly string _path;
        private readonly ISession _session;

        public DbAddService(INHibernateSessionService nHibernateSessionService)
        {
            _session = nHibernateSessionService.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public async void Add(EntityBase entity)
        {
            try
            {
                    await _session.SaveAsync(entity);
                    await _session.FlushAsync();
            }
            catch
            {
                throw new DbInteractionException(_path,"AddUser()");
            }
        } 
    }
}