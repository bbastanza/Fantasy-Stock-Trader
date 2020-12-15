using System;

namespace Core.Entities
{
    public class UserSession : EntityBase
    {
        public UserSession()
        {
        }
        
        public virtual string GuidString { get; set; }
        public virtual DateTime InitDateTime { get; set; }
        public virtual DateTime ExpireDateTime { get; set; }
        public virtual User User { get; set; }
        
    }
}