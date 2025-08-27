using System;
using api.DTOs.Rentals;
using api.DTOs.Users;

namespace api.DTOs.Invoice
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public int RentalId { get; set; }
        public int UserId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int? PaymentStatus { get; set; }
        public RentalDTO Rental { get; set; } = null!;
        public StaffLesseDTO User { get; set; } = null!;
    }

    public class CreateInvoiceDTO
    {
        public int RentalId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public int? PaymentStatus { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
    }

    public class UpdateInvoiceDTO
    {
        public int RentalId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public int? PaymentStatus { get; set; }
    }
}