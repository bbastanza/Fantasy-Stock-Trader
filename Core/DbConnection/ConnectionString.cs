namespace Core.DbConnection
{
    public interface IGetConnectionString
    {
        string ConnString();
    }

    public class GetConnectionString : IGetConnectionString
    {
        public string ConnString()
        {
            return
                "User ID=postgres;Password=password;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
        }
    }
}