using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class UpdatePriceConfigurationFKv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceConfigurations_TypePrices_TypePriceId",
                table: "PriceConfigurations");

            migrationBuilder.DropColumn(
                name: "TypePriceÍd",
                table: "PriceConfigurations");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypePriceId",
                table: "PriceConfigurations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceConfigurations_TypePrices_TypePriceId",
                table: "PriceConfigurations",
                column: "TypePriceId",
                principalTable: "TypePrices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceConfigurations_TypePrices_TypePriceId",
                table: "PriceConfigurations");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypePriceId",
                table: "PriceConfigurations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TypePriceÍd",
                table: "PriceConfigurations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceConfigurations_TypePrices_TypePriceId",
                table: "PriceConfigurations",
                column: "TypePriceId",
                principalTable: "TypePrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
