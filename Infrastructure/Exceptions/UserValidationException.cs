namespace Infrastructure.Exceptions
{
    public class UserValidationException : DreamTraderException
    {
        public UserValidationException(string path, string method)
        :base(path,method, "There was a validation exception. Input password != user's password")
        {
        }
    }
}