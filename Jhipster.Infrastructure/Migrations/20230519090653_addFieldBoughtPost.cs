using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class addFieldBoughtPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "SalePosts",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "BoughtPosts",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BoughtPosts",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "BoughtPosts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "BoughtPosts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titile",
                table: "BoughtPosts",
                type: "character varying(25)",
                maxLength: 25,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "BoughtPosts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BoughtPosts");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "BoughtPosts");

            migrationBuilder.DropColumn(
                name: "Titile",
                table: "BoughtPosts");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "SalePosts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "BoughtPosts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
