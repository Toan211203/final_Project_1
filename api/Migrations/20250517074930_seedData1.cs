using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class seedData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "PublisherID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "PublisherID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "PublisherID",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherID", "ContactPerson", "CreatedAt", "Email", "PhoneNumber", "PublisherName" },
                values: new object[,]
                {
                    { 1, "Alice Johnson", new DateTime(2025, 5, 17, 14, 41, 11, 811, DateTimeKind.Local).AddTicks(4526), "alice@penguin.com", "1234567890", "Penguin Random House" },
                    { 2, "Bob Smith", new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(15), "bob@harpercollins.com", "0987654321", "HarperCollins" },
                    { 3, "Carol White", new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(32), "carol@simon.com", "1122334455", "Simon & Schuster" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "CreatedAt", "Email", "FullName", "Password", "PhoneNumber", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "123 Admin St", new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(9112), "admin1@example.com", "System Administrator", "admin123", "0900000001", 1, "admin1" },
                    { 2, "456 Staff Rd", new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(9494), "staff1@example.com", "Library Staff", "staff123", "0900000002", 2, "staff1" },
                    { 3, "789 Lesse Ave", new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(9501), "lesse1@example.com", "John Lesse", "lesse123", "0900000003", 3, "lesse1" },
                    { 4, "101 Reader Ln", new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(9504), "lesse2@example.com", "Jane Reader", "lesse456", "0900000004", 3, "lesse2" }
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
        }
    }
}
