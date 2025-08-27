using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class seedData4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "Email", "FullName", "Password", "PhoneNumber", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "123 Admin St", "admin1@example.com", "System Administrator", "123", "0900000001", 1, "admin1" },
                    { 2, "456 Staff Rd", "staff1@example.com", "Library Staff", "staff123", "0900000002", 2, "staff1" },
                    { 3, "789 Lesse Ave", "lesse1@example.com", "John Lesse", "lesse123", "0900000003", 3, "lesse1" },
                    { 4, "101 Reader Ln", "lesse2@example.com", "Jane Reader", "lesse456", "0900000004", 3, "lesse2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
