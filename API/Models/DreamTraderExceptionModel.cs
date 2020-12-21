using Infrastructure.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class DreamTraderExceptionModel
    {
        public DreamTraderExceptionModel(DreamTraderException ex)
        {
            FriendlyMessage = $"{ex.FriendlyMessage}";
        }
        
        public string FriendlyMessage { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
