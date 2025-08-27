using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitDB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "PublisherID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 41, 11, 811, DateTimeKind.Local).AddTicks(4526));

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "PublisherID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "PublisherID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(32));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(9112));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(9494));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(9501));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 41, 11, 813, DateTimeKind.Local).AddTicks(9504));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "PublisherID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 38, 59, 66, DateTimeKind.Local).AddTicks(8247));

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "PublisherID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(1420));

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "PublisherID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(1433));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(9167));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(9511));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(9516));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 14, 38, 59, 68, DateTimeKind.Local).AddTicks(9519));
        }
    }
}
