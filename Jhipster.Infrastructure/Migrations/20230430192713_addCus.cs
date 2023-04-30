using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class addCus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Wallets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "WalletPromotionals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Company = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsUnique = table.Column<bool>(type: "boolean", nullable: true),
                    Avatar = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true),
                    Exchange = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ExchangeDescription = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true),
                    MaintainFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MaintainTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CustomerId",
                table: "Wallets",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletPromotionals_CustomerId",
                table: "WalletPromotionals",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletPromotionals_Customers_CustomerId",
                table: "WalletPromotionals",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Customers_CustomerId",
                table: "Wallets",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletPromotionals_Customers_CustomerId",
                table: "WalletPromotionals");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Customers_CustomerId",
                table: "Wallets");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_CustomerId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_WalletPromotionals_CustomerId",
                table: "WalletPromotionals");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "WalletPromotionals");
        }
    }
}
