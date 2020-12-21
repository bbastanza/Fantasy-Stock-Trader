namespace Infrastructure.Exceptions
{
    public class InvalidInputException : DreamTraderException
    {
        public InvalidInputException(string path, string method)
            : base(path,method,"Invalid Input", "The data that you have inputted is invalid. Please try again.")
        {
        }
    }
}
