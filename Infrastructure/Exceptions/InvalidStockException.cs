namespace Infrastructure.Exceptions
{
    public class InvalidStockException : DreamTraderException
    {
        public InvalidStockException(string path, string method)
            : base(path,method,"Invalid Input", "The stock you are looking for is invalid. Try again.")
        {
        }
    }
}
