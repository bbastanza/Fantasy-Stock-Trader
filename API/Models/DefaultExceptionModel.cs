using System;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class DefaultExceptionModel
    {
        public DefaultExceptionModel(Exception ex)
        {
            Type = ex.GetType().ToString();
            Message = $"{ex.Message}";
            StackTrace = ex.StackTrace;
        }

        public string Type { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
