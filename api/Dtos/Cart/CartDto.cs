using System;
using api.DTOs.Book;
using api.DTOs.Users;

namespace api.DTOs.Cart
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime? AddedAt { get; set; }
        public BookDTO Book { get; set; } = null!;
        public StaffLesseDTO User { get; set; } = null!;
    }

    public class CreateCartDTO
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        //public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdateCartDTO
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}