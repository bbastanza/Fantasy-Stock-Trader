namespace Infrastructure.Exceptions
{
    public class StockTransactionException : DreamTraderException
    {
        public StockTransactionException(string path, string method)
            : base(path, method, "There was an error within the transaction")
        {
        }
    }
}