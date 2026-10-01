using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class RenameEmployeesToLibrarians : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Employees_EmployeeId",
                table: "Borrows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Librarians");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Borrows",
                newName: "LibrarianId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrows_EmployeeId",
                table: "Borrows",
                newName: "IX_Borrows_LibrarianId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Librarians",
                newName: "LibrarianId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_Username",
                table: "Librarians",
                newName: "IX_Librarians_Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Librarians",
                table: "Librarians",
                column: "LibrarianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrows_Librarians_LibrarianId",
                table: "Borrows",
                column: "LibrarianId",
                principalTable: "Librarians",
                principalColumn: "LibrarianId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Librarians_LibrarianId",
                table: "Borrows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Librarians",
                table: "Librarians");

            migrationBuilder.RenameTable(
                name: "Librarians",
                newName: "Employees");

            migrationBuilder.RenameColumn(
                name: "LibrarianId",
                table: "Borrows",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrows_LibrarianId",
                table: "Borrows",
                newName: "IX_Borrows_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "LibrarianId",
                table: "Employees",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Librarians_Username",
                table: "Employees",
                newName: "IX_Employees_Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrows_Employees_EmployeeId",
                table: "Borrows",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
