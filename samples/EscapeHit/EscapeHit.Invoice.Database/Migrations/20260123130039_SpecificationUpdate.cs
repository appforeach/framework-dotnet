using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscapeHit.Invoice.Database.Migrations
{
    /// <inheritdoc />
    public partial class SpecificationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerNumber",
                table: "Invoices",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerNumber",
                table: "Invoices",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);
        }
    }
}
