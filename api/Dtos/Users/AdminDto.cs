using System;

namespace api.DTOs.Users
{
    public class AdminDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Role { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateAdminDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdateAdminDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Role { get; set; }
    }
}