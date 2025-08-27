using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string PublisherName { get; set; } = null!;

    public string? ContactPerson { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
