using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class UpdatePriceConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TypePriceId",
                table: "PriceConfigurations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PriceConfigurations_TypePriceId",
                table: "PriceConfigurations",
                column: "TypePriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceConfigurations_TypePrices_TypePriceId",
                table: "PriceConfigurations",
                column: "TypePriceId",
                principalTable: "TypePrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceConfigurations_TypePrices_TypePriceId",
                table: "PriceConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_PriceConfigurations_TypePriceId",
                table: "PriceConfigurations");

            migrationBuilder.DropColumn(
                name: "TypePriceId",
                table: "PriceConfigurations");
        }
    }
}
