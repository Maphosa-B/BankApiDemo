using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class ChangedStatustoIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
