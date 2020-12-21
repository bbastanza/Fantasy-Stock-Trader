using System;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class DefaultExceptionModel
    {
        public DefaultExceptionModel()
        {
            FriendlyMessage = "An unknow error has occured. We appologize for the inconvienience. Please try again.";
        }

        public string FriendlyMessage { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
