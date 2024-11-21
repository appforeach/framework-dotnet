using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppForeach.Framework.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class Audit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit",
                schema: "framework",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    InputAuditId = table.Column<int>(type: "int", nullable: true),
                    OperationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCommand = table.Column<bool>(type: "bit", nullable: false),
                    IsInput = table.Column<bool>(type: "bit", nullable: false),
                    OccuredOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LoggingTraceId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LoggingTransactionId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Outcome = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audit_InputAuditId",
                schema: "framework",
                table: "Audit",
                column: "InputAuditId");

            migrationBuilder.CreateIndex(
                name: "IX_Audit_TransactionId",
                schema: "framework",
                table: "Audit",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit",
                schema: "framework");
        }
    }
}
