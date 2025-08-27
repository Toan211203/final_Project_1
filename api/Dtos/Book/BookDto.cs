using System;
using System.Collections.Generic;
using api.DTOs.Cart;
using api.DTOs.Genre;
using api.DTOs.Publisher;
using api.DTOs.Rentals;
using api.DTOs.Review;


namespace api.DTOs.Book
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int PublisherId { get; set; }
        public string Isbn { get; set; } = null!;
        public int? GenreId { get; set; }
        public int? PublishedYear { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string GenreName { get; set; } = null!;
        public string PublisherName { get; set; } = null!; 
        public ICollection<CartDTO>? Carts { get; set; } = new List<CartDTO>();
        public GenreDto? Genre { get; set; }
        public PublisherDTO? Publisher { get; set; } = null!;
        public ICollection<RentalDetailDTO>? RentalDetails { get; set; } = new List<RentalDetailDTO>();
        public ICollection<ReviewDTO>? Reviews { get; set; } = new List<ReviewDTO>();
    }

    public class CreateBookDTO
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int PublisherId { get; set; }
        public string Isbn { get; set; } = null!;
        public int? GenreId { get; set; }
        public int? PublishedYear { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdateBookDTO
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int PublisherId { get; set; }
        public string Isbn { get; set; } = null!;
        public int? GenreId { get; set; }
        public int? PublishedYear { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}