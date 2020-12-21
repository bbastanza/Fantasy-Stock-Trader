namespace Infrastructure.Exceptions
{
    public class ExpiredSessionException : DreamTraderException
    {
        public ExpiredSessionException(string path, string method)
            : base(path, method, "Session does not exists", "Sorry, your session has expired. Please log in again.")
        {
        }
    }
}
