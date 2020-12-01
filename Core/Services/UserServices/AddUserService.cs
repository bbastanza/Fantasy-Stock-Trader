using System;
using System.IO;
using Core.Entities;
using Core.Services.DbServices;
using Infrastructure.Exceptions;

namespace Core.Services.UserServices
{
    public interface IAddUserService
    {
        User AddUser(string userName, string password, string email);
    }

    public class AddUserService : IAddUserService
    {
        private readonly IDbQueryService _dbQueryService;

        public AddUserService(IDbQueryService dbQueryService)
        {
            _dbQueryService = dbQueryService;
        } 
        public User AddUser(string userName, string password, string email)
        {
            if (userName == null || password == null || email == null)
                throw new InvalidInputException(Path.GetFullPath(ToString()), "AddUser()");

            Console.WriteLine(_dbQueryService.CheckExistingUser(userName));
            
            var newUser = new User(userName, password, email);
            // add newUser to db
            return newUser;
        }
    }
}