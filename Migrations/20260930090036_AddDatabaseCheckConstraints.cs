using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseCheckConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_BorrowDetails_Quantity_Positive",
                table: "BorrowDetails",
                sql: "[Quantity] >= 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Books_AvailableCopies_Lte_TotalCopies",
                table: "Books",
                sql: "[AvailableCopies] <= [TotalCopies]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Books_AvailableCopies_NonNegative",
                table: "Books",
                sql: "[AvailableCopies] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Books_TotalCopies_Positive",
                table: "Books",
                sql: "[TotalCopies] >= 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_BorrowDetails_Quantity_Positive",
                table: "BorrowDetails");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Books_AvailableCopies_Lte_TotalCopies",
                table: "Books");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Books_AvailableCopies_NonNegative",
                table: "Books");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Books_TotalCopies_Positive",
                table: "Books");
        }
    }
}
