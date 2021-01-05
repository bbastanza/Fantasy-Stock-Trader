using System;

namespace API.Models
{
    public class UserSessionModel
    {
        public string SessionId { get; }
        public DateTime ExpireTime { get; }
        
        public UserSessionModel(string sessionId, DateTime expireTime)
        {
            SessionId = sessionId;
            ExpireTime = expireTime;
        }
    }
}