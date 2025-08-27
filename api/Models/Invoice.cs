using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int RentalId { get; set; }

    public int UserId { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int? PaymentStatus { get; set; }

    public virtual Rental Rental { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
