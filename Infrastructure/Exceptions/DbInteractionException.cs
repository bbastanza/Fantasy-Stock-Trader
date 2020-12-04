namespace Infrastructure.Exceptions
{
    public class DbInteractionException : DreamTraderException
    {
        public DbInteractionException(string path, string method)
        :base(path, method, "There was an error interacting with the database")
        {
        } 
    }
}