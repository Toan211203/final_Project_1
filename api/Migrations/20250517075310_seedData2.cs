using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class seedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherID", "ContactPerson", "Email", "PhoneNumber", "PublisherName" },
                values: new object[,]
                {
                    { 1, "Alice Johnson", "alice@penguin.com", "1234567890", "Penguin Random House" },
                    { 2, "Bob Smith", "bob@harpercollins.com", "0987654321", "HarperCollins" },
                    { 3, "Carol White", "carol@simon.com", "1122334455", "Simon & Schuster" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
