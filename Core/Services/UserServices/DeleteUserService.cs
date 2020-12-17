using System.IO;
using System.Linq;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;
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
        private readonly string _path;

        public DeleteUserService(
            INHibernateSession inHibernateSession)
        {
            _session = inHibernateSession.GetSession();
            _path = Path.GetFullPath(ToString());
        }

        public string DeleteUser(string userName, string password)
        {
            if (userName == null || password == null)
                throw new InvalidInputException(Path.GetFullPath(ToString()), "DeleteUser");

            _session.Query<User>()
                .Where(user => user.UserName == userName).Delete();

            return $"{userName} has been deleted from the database";
        }
    }
}