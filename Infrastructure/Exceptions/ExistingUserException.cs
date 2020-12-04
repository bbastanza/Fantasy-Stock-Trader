namespace Infrastructure.Exceptions
{
    public class ExistingUserException : DreamTraderException
    {
        public ExistingUserException(string path, string method) 
            : base(path, method, "User already exists")
        {
        }
    }
}