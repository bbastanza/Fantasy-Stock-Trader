using System;

namespace API.Models
{
    public class UserInputModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }    
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}