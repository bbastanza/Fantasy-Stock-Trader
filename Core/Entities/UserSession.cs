using System;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Entities
{
    public class UserSession : EntityBase
    {
        public UserSession()
        {
        }
        
        public virtual string SessionId { get; set; }
        public virtual DateTime InitDateTime { get; set; }
        public virtual DateTime ExpireDateTime { get; set; }
        public virtual User User { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}