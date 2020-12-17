using System;

namespace API.Models
{
    public class UserSessionModel
    {
        public string SessionId { get; set; }
        public DateTime ExpireTime { get; set; }
        
        public UserSessionModel(string sessionId, DateTime expireTime)
        {
            SessionId = sessionId;
            ExpireTime = expireTime;
        }
    }
}