using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Book;

namespace api.DTOs.Rentals
{
    public class RentalDetailDTO
    {
        public int RentalDetailId { get; set; }
        public int RentalId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public string BookTitle { get; set; } = null!;
        public BookDTO Book { get; set; } = null!;
        public RentalDTO Rental { get; set; } = null!;
    }

    public class UpsertRentalDetailDTO
    {
        public int RentalId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}