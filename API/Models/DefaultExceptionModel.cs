using System;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class DefaultExceptionModel
    {
        public string Type { get; }
        public string Message { get; }
        
        public DefaultExceptionModel(Exception ex)
        {
            Type = ex.GetType().ToString();
            Message = $"{ex.Message}";
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
