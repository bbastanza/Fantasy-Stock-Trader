namespace Infrastructure.Exceptions
{
    public class NonExistingHoldingException : DreamTraderException
    {
        public NonExistingHoldingException(string path, string method)
        :base(path,method, "You are trying to sell shares of a holding you do not own.", "Sorry, you cannot sell shares of a stock you do not own.")
        {
        }
    }
}
