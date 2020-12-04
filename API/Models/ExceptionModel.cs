using System.Text.Json.Serialization;
using Infrastructure.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class ExceptionModel
    {
        public string Type { get; set; }
        public string Path { get; set; }
        public string Message { get; set; }
        
        public ExceptionModel(DreamTraderException ex)
        {
            Type = ex.GetType().ToString();
            Path = $"{ex.Path}.{ex.Method}";
            Message = $"{ex.Message}";
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}