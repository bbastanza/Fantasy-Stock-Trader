using System;

namespace Infrastructure.Exceptions
{
    public abstract class DreamTraderException : Exception
    {
        public string Path { get; }
        public string Method { get; }
        public string ClientMessage { get; }

        internal DreamTraderException(string path, string method, string message, string clientMessage)
            : base(message)
        {
            Path = path;
            Method = method;
            ClientMessage = clientMessage;
        }
    }
}