using System;
using System.Collections.Generic;
using api.DTOs.Invoice;
using api.DTOs.Users;

namespace api.DTOs.Rentals
{
    public class RentalDTO
    {
        public int RentalId { get; set; }
        public int UserId { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? Status { get; set; }
        public decimal TotalCost { get; set; }
        public ICollection<InvoiceDTO>? Invoices { get; set; } = new List<InvoiceDTO>();
        public ICollection<RentalDetailDTO>? RentalDetails { get; set; } = new List<RentalDetailDTO>();
        public StaffLesseDTO User { get; set; } = null!;
    }

    public class CreateRentalDTO
    {
        public int UserId { get; set; }
        //public DateTime? RentalDate { get; set; }
        //public DateTime DueDate { get; set; }
        public decimal TotalCost { get; set; }
        public int? Status { get; set; }
        public List<UpsertRentalDetailDTO>? RentalDetails { get; set; } = new();
    }

    public class UpdateRentalDTO
    {
        public int UserId { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal TotalCost { get; set; }
        public int? Status { get; set; }
        
    }
}