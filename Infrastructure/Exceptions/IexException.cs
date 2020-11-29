namespace Infrastructure.Exceptions
{
    public class IexException : DreamTraderException
    {
        public IexException(string path, string method) 
            : base(path, method, "There was an error fetching data from the Iex Api")
        {
        }
    }
}