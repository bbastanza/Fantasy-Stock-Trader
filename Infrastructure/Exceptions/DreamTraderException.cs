using System;

namespace Infrastructure.Exceptions
{
    public abstract class DreamTraderException : Exception
    {
        public string Path { get; }
        public string Method { get; }

        internal DreamTraderException(string path, string method, string message)
            : base(message)
        {
            Path = path;
            Method = method;
        }
    }
}