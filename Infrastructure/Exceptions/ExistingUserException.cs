namespace Infrastructure.Exceptions
{
    public class ExistingUserException : DreamTraderException
    {
        public ExistingUserException(string path, string method)
            : base(path, method, "A user with that name already exists", "Sorry, a user with that name already exists. Please pick a different username.")
        {
        }
    }
}
