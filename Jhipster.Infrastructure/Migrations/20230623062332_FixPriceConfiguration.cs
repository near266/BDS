using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class FixPriceConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "PriceConfigurations");

            migrationBuilder.AddColumn<int>(
                name: "Date",
                table: "PriceConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "PriceConfigurations");

            migrationBuilder.AddColumn<DateTime>(
                name: "Day",
                table: "PriceConfigurations",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
