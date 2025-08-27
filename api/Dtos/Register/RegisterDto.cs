using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Register
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}