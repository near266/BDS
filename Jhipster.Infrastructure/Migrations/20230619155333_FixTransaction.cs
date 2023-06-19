using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class FixTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "TransactionHistorys",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TransactionHistorys",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Walletamount",
                table: "TransactionHistorys",
                type: "numeric",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "TransactionHistorys");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "TransactionHistorys");

            migrationBuilder.DropColumn(
                name: "Walletamount",
                table: "TransactionHistorys");
        }
    }
}
