using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class seedData3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
