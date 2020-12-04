namespace Infrastructure.Exceptions
{
    public class NonExistingUserException : DreamTraderException
    {
        public NonExistingUserException(string path, string method) 
            : base(path, method, "User does not exists")
        {
        }
    }
}
