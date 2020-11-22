using System;

namespace Core.DbConnection
{
    public class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void OpenConnection()
        {
            Console.WriteLine($"Opening Postgres Connection at ConnString: {_connectionString} ");
        }

        public void CloseConnection()
        {
            Console.WriteLine($"Closing Postgres Connection at ConnString: {_connectionString} "); 
        }
    }
}