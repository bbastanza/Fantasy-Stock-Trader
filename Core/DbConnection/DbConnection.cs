using System;

namespace Core.DbConnection
{
    public class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection()
        {
            _connectionString =  "User ID=postgres;Password=password;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
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
// User ID=postgres;Password=password;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;}