using Infrastructure.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class DreamTraderExceptionModel
    {
        public DreamTraderExceptionModel(DreamTraderException ex)
        {
            ClientMessage = $"{ex.ClientMessage}";
        }
        
        public string ClientMessage { get; }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
