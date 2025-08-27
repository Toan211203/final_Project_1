using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Book
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

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Genre? Genre { get; set; }

    public virtual Publisher Publisher { get; set; } = null!;

    public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
