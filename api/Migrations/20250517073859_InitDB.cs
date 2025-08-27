using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genres__0385055EF2F7C936", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    PublisherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ContactPerson = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Publishe__4C657E4B7E835C27", x => x.PublisherID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC723A07D8", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Author = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PublisherID = table.Column<int>(type: "int", nullable: false),
                    ISBN = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    GenreID = table.Column<int>(type: "int", nullable: true),
                    PublishedYear = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Books__3DE0C22765CA3FF4", x => x.BookID);
                    table.ForeignKey(
                        name: "FK__Books__GenreID__44FF419A",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "GenreID");
                    table.ForeignKey(
                        name: "FK__Books__Publisher__440B1D61",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "PublisherID");
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    RentalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RentalDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    TotalCost = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rentals__97005963BDC801CC", x => x.RentalID);
                    table.ForeignKey(
                        name: "FK__Rentals__UserID__4F7CD00D",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    AddedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__51BCD79721FD00CE", x => x.CartID);
                    table.ForeignKey(
                        name: "FK__Cart__BookID__4AB81AF0",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__Cart__UserID__49C3F6B7",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__74BC79AEB3AE28FC", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK__Reviews__BookID__5FB337D6",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__Reviews__UserID__5EBF139D",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invoices__D796AAD5ED2F1A10", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK__Invoices__Rental__59063A47",
                        column: x => x.RentalID,
                        principalTable: "Rentals",
                        principalColumn: "RentalID");
                    table.ForeignKey(
                        name: "FK__Invoices__UserID__59FA5E80",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "RentalDetails",
                columns: table => new
                {
                    RentalDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RentalDe__10B35999A4E4B474", x => x.RentalDetailID);
                    table.ForeignKey(
                        name: "FK__RentalDet__BookI__5441852A",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__RentalDet__Renta__534D60F1",
                        column: x => x.RentalID,
                        principalTable: "Rentals",
                        principalColumn: "RentalID");
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreID", "GenreName" },
                values: new object[,]
                {
                    { 1, "Science Fiction" },
                    { 2, "Fantasy" },
                    { 3, "Mystery" },
                    { 4, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherID", "ContactPerson", "CreatedAt", "Email", "PhoneNumber", "PublisherName" },
                values: new object[,]
                {
                    { 1, "Alice Johnson", new DateTime(2025, 5, 17, 14, 38, 59, 66, DateTimeKind.Local).AddTicks(8247), "alice@penguin.com", "1234567890", "Penguin Random House" },
                    { 2, "Bob Smith", new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(1420), "bob@harpercollins.com", "0987654321", "HarperCollins" },
                    { 3, "Carol White", new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(1433), "carol@simon.com", "1122334455", "Simon & Schuster" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "CreatedAt", "Email", "FullName", "Password", "PhoneNumber", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "123 Admin St", new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(9167), "admin1@example.com", "System Administrator", "admin123", "0900000001", 1, "admin1" },
                    { 2, "456 Staff Rd", new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(9511), "staff1@example.com", "Library Staff", "staff123", "0900000002", 2, "staff1" },
                    { 3, "789 Lesse Ave", new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(9516), "lesse1@example.com", "John Lesse", "lesse123", "0900000003", 3, "lesse1" },
                    { 4, "101 Reader Ln", new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(9519), "lesse2@example.com", "Jane Reader", "lesse456", "0900000004", 3, "lesse2" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Author", "GenreID", "ISBN", "Price", "PublishedYear", "PublisherID", "Quantity", "Title" },
                values: new object[,]
                {
                    { 1, "H.G. Wells", 1, "9780451528551", 50000m, null, 1, 0, "The Time Machine" },
                    { 2, "Frank Herbert", 1, "9780441172719", 45000m, null, 2, 0, "Dune" },
                    { 3, "J.R.R. Tolkien", 2, "9780547928227", 40000m, null, 1, 0, "The Hobbit" },
                    { 4, "Patrick Rothfuss", 2, "9780756404741", 40000m, null, 3, 0, "The Name of the Wind" },
                    { 5, "Gillian Flynn", 3, "9780307588371", 35000m, null, 2, 0, "Gone Girl" },
                    { 6, "Stieg Larsson", 3, "9780307454546", 45000m, null, 3, 0, "The Girl with the Dragon Tattoo" },
                    { 7, "Jane Austen", 4, "9780141439518", 20000m, null, 1, 0, "Pride and Prejudice" },
                    { 8, "Nicholas Sparks", 4, "9780446605236", 20000m, null, 2, 0, "The Notebook" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreID",
                table: "Books",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherID",
                table: "Books",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "UQ__Books__447D36EAD9848AFF",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_BookID",
                table: "Cart",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserID",
                table: "Cart",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Genres__BBE1C33950C3A98F",
                table: "Genres",
                column: "GenreName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_RentalID",
                table: "Invoices",
                column: "RentalID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserID",
                table: "Invoices",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RentalDetails_BookID",
                table: "RentalDetails",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_RentalDetails_RentalID",
                table: "RentalDetails",
                column: "RentalID");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_UserID",
                table: "Rentals",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookID",
                table: "Reviews",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E4C97A627C",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D1053414EDCF60",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "RentalDetails");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
