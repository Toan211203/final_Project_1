using System;
using System.Collections.Generic;
using api.DTOs.Book;

namespace api.DTOs.Publisher
{
    public class PublisherDTO
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = null!;
        public string? ContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<BookDTO>? Books { get; set; } = new List<BookDTO>();
    }

    public class CreatePublisherDTO
    {
        public string PublisherName { get; set; } = null!;
        public string? ContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdatePublisherDTO
    {
        public string PublisherName { get; set; } = null!;
        public string? ContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}