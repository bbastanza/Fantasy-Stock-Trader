namespace Infrastructure.Exceptions
{
    public class InvalidSymbolException : DreamTraderException
    {
        public InvalidSymbolException(string path, string method)
            : base(path,method,"Invalid Input", "The stock you are looking for is invalid. Try again.")
        {
        }
    }
}
