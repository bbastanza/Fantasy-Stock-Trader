using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using NHibernate;
using NHibernate.Linq;

namespace Core.Services.UserServices
{
    public interface IDeleteUserService
    {
        string DeleteUser(string userName, string password);
    }

    public class DeleteUserService : IDeleteUserService
    {
        private readonly ISession _session;

        public DeleteUserService(
            INHibernateSession inHibernateSession)
        {
            _session = inHibernateSession.GetSession();
        }

        public string DeleteUser(string userName, string password)
        {
            _session.Query<User>()
                .Where(user => user.UserName == userName).Delete();

            return $"{userName} has been deleted from the database";
        }
    }
}