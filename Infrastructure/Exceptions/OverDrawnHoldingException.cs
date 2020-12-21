namespace Infrastructure.Exceptions
{
    public class OverDrawnHoldingException : DreamTraderException
    {
        public OverDrawnHoldingException(string path, string method)
            :base(path,method, "You are trying to sell too many shares. Use (sellAll == true) to sell all shares", "Sorry, your transaction did not confirm because you do not have enough shares.")
        {
        }
    }
}
