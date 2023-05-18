using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class addCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DeliveryDatas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DeliveryDatas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "DeliveryDatas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "DeliveryDatas",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DeliveryDatas");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DeliveryDatas");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "DeliveryDatas");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "DeliveryDatas");
        }
    }
}
