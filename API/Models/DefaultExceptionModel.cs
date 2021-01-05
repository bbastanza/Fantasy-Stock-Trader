using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class DefaultExceptionModel
    {
        public DefaultExceptionModel()
        {
            ClientMessage = "An unknown error has occured. We apologize for the inconvenience. Please try again.";
        }

        public string ClientMessage { get; }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
