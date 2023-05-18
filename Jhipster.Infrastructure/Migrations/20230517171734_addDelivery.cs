using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class addDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OTP",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Area",
                table: "SalePosts",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "SalePosts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true),
                    Method = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MethodData = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false),
                    IsSend = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDatas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryDatas");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "SalePosts");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "SalePosts");
        }
    }
}
