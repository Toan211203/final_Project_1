using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class fixUserID1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Cart__UserID__49C3F6B7",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK__Invoices__UserID__59FA5E80",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK__Rentals__UserID__4F7CD00D",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK__Reviews__UserID__5EBF139D",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Reviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Rentals",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_UserID",
                table: "Rentals",
                newName: "IX_Rentals_UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Invoices",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_UserID",
                table: "Invoices",
                newName: "IX_Invoices_UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Cart",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_UserID",
                table: "Cart",
                newName: "IX_Cart_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__Cart__UserId__49C3F6B7",
                table: "Cart",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__Invoices__UserId__59FA5E80",
                table: "Invoices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__Rentals__UserId__4F7CD00D",
                table: "Rentals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__Reviews__UserId__5EBF139D",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Cart__UserId__49C3F6B7",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK__Invoices__UserId__59FA5E80",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK__Rentals__UserId__4F7CD00D",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK__Reviews__UserId__5EBF139D",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reviews",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                newName: "IX_Reviews_UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Rentals",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_UserId",
                table: "Rentals",
                newName: "IX_Rentals_UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Invoices",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                newName: "IX_Invoices_UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cart",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                newName: "IX_Cart_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK__Cart__UserID__49C3F6B7",
                table: "Cart",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK__Invoices__UserID__59FA5E80",
                table: "Invoices",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK__Rentals__UserID__4F7CD00D",
                table: "Rentals",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK__Reviews__UserID__5EBF139D",
                table: "Reviews",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
