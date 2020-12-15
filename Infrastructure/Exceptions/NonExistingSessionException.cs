namespace Infrastructure.Exceptions
{
    public class NonExistingSessionException : DreamTraderException
    {
        public NonExistingSessionException(string path, string method)
            : base(path, method, "Session does not exists")
        {
        }
    }
}