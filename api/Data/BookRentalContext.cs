using System;
using System.Collections.Generic;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api.Data;

public partial class BookRentalContext : DbContext
{
    public BookRentalContext()
    {
    }

    public BookRentalContext(DbContextOptions<BookRentalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<RentalDetail> RentalDetails { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C22765CA3FF4");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EAD9848AFF").IsUnique();

            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PublisherId).HasColumnName("PublisherID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Books__GenreID__44FF419A");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__Publisher__440B1D61");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD79721FD00CE");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.Book).WithMany(p => p.Carts)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__BookID__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__UserId__49C3F6B7");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385055EF2F7C936");

            entity.HasIndex(e => e.GenreName, "UQ__Genres__BBE1C33950C3A98F").IsUnique();

            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.GenreName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAD5ED2F1A10");

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus).HasDefaultValue(0);
            entity.Property(e => e.RentalId).HasColumnName("RentalID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.Rental).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__Rental__59063A47");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__UserId__59FA5E80");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__4C657E4B7E835C27");

            entity.Property(e => e.PublisherId).HasColumnName("PublisherID");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.PublisherName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.RentalId).HasName("PK__Rentals__97005963BDC801CC");

            entity.Property(e => e.RentalId).HasColumnName("RentalID");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.RentalDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(0);
            entity.Property(e => e.TotalCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rentals__UserId__4F7CD00D");
        });

        modelBuilder.Entity<RentalDetail>(entity =>
        {
            entity.HasKey(e => e.RentalDetailId).HasName("PK__RentalDe__10B35999A4E4B474");

            entity.Property(e => e.RentalDetailId).HasColumnName("RentalDetailID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.RentalId).HasColumnName("RentalID");

            entity.HasOne(d => d.Book).WithMany(p => p.RentalDetails)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RentalDet__BookI__5441852A");

            entity.HasOne(d => d.Rental).WithMany(p => p.RentalDetails)
                .HasForeignKey(d => d.RentalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RentalDet__Renta__534D60F1");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEB3AE28FC");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.Rating).HasDefaultValue(1);
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.Book).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__BookID__5FB337D6");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__UserId__5EBF139D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC723A07D8");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4C97A627C").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053414EDCF60").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Genre>().HasData(
            new Genre { GenreId = 1, GenreName = "Science Fiction" },
            new Genre { GenreId = 2, GenreName = "Fantasy" },
            new Genre { GenreId = 3, GenreName = "Mystery" },
            new Genre { GenreId = 4, GenreName = "Romance" }
        );

        modelBuilder.Entity<Publisher>().HasData(
            new Publisher
            {
                PublisherId = 1,
                PublisherName = "Penguin Random House",
                ContactPerson = "Alice Johnson",
                Email = "alice@penguin.com",
                PhoneNumber = "1234567890"
            },
            new Publisher
            {
                PublisherId = 2,
                PublisherName = "HarperCollins",
                ContactPerson = "Bob Smith",
                Email = "bob@harpercollins.com",
                PhoneNumber = "0987654321"
            },
            new Publisher
            {
                PublisherId = 3,
                PublisherName = "Simon & Schuster",
                ContactPerson = "Carol White",
                Email = "carol@simon.com",
                PhoneNumber = "1122334455"
            }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                BookId = 1,
                Title = "The Time Machine",
                Author = "H.G. Wells",
                Isbn = "9780451528551",
                Price = 50000,
                GenreId = 1,
                PublisherId = 1
            },
            new Book
            {
                BookId = 2,
                Title = "Dune",
                Author = "Frank Herbert",
                Isbn = "9780441172719",
                Price = 45000,
                GenreId = 1,
                PublisherId = 2
            },
            new Book
            {
                BookId = 3,
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Isbn = "9780547928227",
                Price = 40000,
                GenreId = 2,
                PublisherId = 1
            },
            new Book
            {
                BookId = 4,
                Title = "The Name of the Wind",
                Author = "Patrick Rothfuss",
                Isbn = "9780756404741",
                Price = 40000,
                GenreId = 2,
                PublisherId = 3
            },
            new Book
            {
                BookId = 5,
                Title = "Gone Girl",
                Author = "Gillian Flynn",
                Isbn = "9780307588371",
                Price = 35000,
                GenreId = 3,
                PublisherId = 2
            },
            new Book
            {
                BookId = 6,
                Title = "The Girl with the Dragon Tattoo",
                Author = "Stieg Larsson",
                Isbn = "9780307454546",
                Price = 45000,
                GenreId = 3,
                PublisherId = 3
            },
            new Book
            {
                BookId = 7,
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                Isbn = "9780141439518",
                Price = 20000,
                GenreId = 4,
                PublisherId = 1
            },
            new Book
            {
                BookId = 8,
                Title = "The Notebook",
                Author = "Nicholas Sparks",
                Isbn = "9780446605236",
                Price = 20000,
                GenreId = 4,
                PublisherId = 2
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                Username = "admin1",
                Password = "123",
                Email = "admin1@example.com",
                FullName = "System Administrator",
                PhoneNumber = "0900000001",
                Address = "123 Admin St",
                Role = 1
            },
            new User
            {
                UserId = 2,
                Username = "staff1",
                Password = "staff123",
                Email = "staff1@example.com",
                FullName = "Library Staff",
                PhoneNumber = "0900000002",
                Address = "456 Staff Rd",
                Role = 2
            },
            new User
            {
                UserId = 3,
                Username = "lesse1",
                Password = "lesse123",
                Email = "lesse1@example.com",
                FullName = "John Lesse",
                PhoneNumber = "0900000003",
                Address = "789 Lesse Ave",
                Role = 0
            },
            new User
            {
                UserId = 4,
                Username = "lesse2",
                Password = "lesse456",
                Email = "lesse2@example.com",
                FullName = "Jane Reader",
                PhoneNumber = "0900000004",
                Address = "101 Reader Ln",
                Role = 0
            }
        );

    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
