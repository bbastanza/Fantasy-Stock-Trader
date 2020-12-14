using System;

namespace Core.Entities
{
    public class UserSession
    {
        public Guid Guid { get; set; }
        public DateTime InitDateTime { get; set; }
        public DateTime ExpireDateTime { get; set; }
        
        public UserSession()
        {
            Guid = Guid.NewGuid();
            InitDateTime = DateTime.Now;
            ExpireDateTime = DateTime.Now.AddDays(1);
        }
    }
}