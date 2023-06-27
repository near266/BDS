using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class UpdatePriceConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "PriceConfigurations",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceDefault",
                table: "PriceConfigurations",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "PriceConfigurations",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "PriceConfigurations");

            migrationBuilder.DropColumn(
                name: "PriceDefault",
                table: "PriceConfigurations");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "PriceConfigurations");
        }
    }
}
