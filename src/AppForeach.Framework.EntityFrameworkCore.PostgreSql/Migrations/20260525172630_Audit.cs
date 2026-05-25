using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppForeach.Framework.EntityFrameworkCore.PostgreSql.Migrations
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionId = table.Column<int>(type: "integer", nullable: true),
                    InputAuditId = table.Column<int>(type: "integer", nullable: true),
                    OperationName = table.Column<string>(type: "text", nullable: false),
                    IsCommand = table.Column<bool>(type: "boolean", nullable: false),
                    IsInput = table.Column<bool>(type: "boolean", nullable: false),
                    OccuredOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LoggingTraceId = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LoggingTransactionId = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Outcome = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Payload = table.Column<string>(type: "text", nullable: true)
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
