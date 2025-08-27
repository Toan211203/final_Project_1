using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Cart;
using api.DTOs.Invoice;
using api.DTOs.Rentals;
using api.DTOs.Review;

namespace api.DTOs.Users
{
    public class StaffLesseDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public int Role { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<CartDTO>? Carts { get; set; } = new List<CartDTO>();
        public ICollection<InvoiceDTO>? Invoices { get; set; } = new List<InvoiceDTO>();
        public ICollection<RentalDTO>? Rentals { get; set; } = new List<RentalDTO>();
        public ICollection<ReviewDTO>? Reviews { get; set; } = new List<ReviewDTO>();
    }

    public class CreateStaffLesseeDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public int Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdateStaffLesseeDTO
    {
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}