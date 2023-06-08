using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class initHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionHistorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    Content = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TransactionAmount = table.Column<double>(type: "double precision", nullable: true),
                    WalletType = table.Column<int>(type: "integer", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionHistorys_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistorys_CustomerId",
                table: "TransactionHistorys",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionHistorys");
        }
    }
}
