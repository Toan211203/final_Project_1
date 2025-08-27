using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Book;
using api.DTOs.Users;

namespace api.DTOs.Review
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public int BookId { get; set; }
        public string BookTitle { get; set; } = null!;
        public string Username { get; set; } = null!;
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? ReviewDate { get; set; }
        public BookDTO Book { get; set; } = null!;
        public StaffLesseDTO User { get; set; } = null!;
    }

    public class CreateReviewDTO
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? ReviewDate { get; set; } = DateTime.UtcNow;
    }

    public class UpdateReviewDTO
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
    }
}