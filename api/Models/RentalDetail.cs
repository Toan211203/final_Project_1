using System;
using System.Collections.Generic;

namespace api.Models;

public partial class RentalDetail
{
    public int RentalDetailId { get; set; }

    public int RentalId { get; set; }

    public int BookId { get; set; }

    public int Quantity { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Rental Rental { get; set; } = null!;
}
