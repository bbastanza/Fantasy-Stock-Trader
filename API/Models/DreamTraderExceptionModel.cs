using Infrastructure.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class DreamTraderExceptionModel
    {
        public DreamTraderExceptionModel(DreamTraderException ex)
        {
            Type = ex.GetType().ToString();
            Path = $"{ex.Path}.{ex.Method}";
            Message = $"{ex.Message}";
        }
        
        public string Type { get; set; }
        public string Path { get; set; }
        public string Message { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}