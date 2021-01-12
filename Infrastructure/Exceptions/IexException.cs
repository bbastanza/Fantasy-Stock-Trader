namespace Infrastructure.Exceptions
{
    public class IexException : DreamTraderException
    {
        public IexException(string path, string method)
            : base(path,method,"Iex Error", "The stock you are looking for is invalid. Try again.")
        {
        }
    }
}
