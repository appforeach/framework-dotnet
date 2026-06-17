using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppForeach.Framework.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class Audit_Add_Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Audit_OccuredOn",
                schema: "framework",
                table: "Audit",
                column: "OccuredOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Audit_OccuredOn",
                schema: "framework",
                table: "Audit");
        }
    }
}
