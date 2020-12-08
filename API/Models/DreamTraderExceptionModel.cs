using Infrastructure.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class DreamTraderExceptionModel
    {
        public string Type { get; }
        public string Path { get; }
        public string Message { get; }
        
        public DreamTraderExceptionModel(DreamTraderException ex)
        {
            Type = ex.GetType().ToString();
            Path = $"{ex.Path}.{ex.Method}";
            Message = $"{ex.Message}";
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}