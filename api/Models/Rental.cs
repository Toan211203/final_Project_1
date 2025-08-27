using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Rental
{
    public int RentalId { get; set; }

    public int UserId { get; set; }

    public DateTime? RentalDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int? Status { get; set; }

    public decimal TotalCost { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();

    public virtual User User { get; set; } = null!;
}
