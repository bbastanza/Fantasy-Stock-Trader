namespace Infrastructure.Exceptions
{
    public class InvalidSymbolException : DreamTraderException
    {
        public InvalidSymbolException(string path, string method)
            : base(path, method, "Null Symbol", "Sorry, You have entered incomplete data. Please Try Again.")
        {
        }
    }
}