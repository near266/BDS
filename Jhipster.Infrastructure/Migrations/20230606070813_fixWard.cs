using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class fixWard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wards_Districts_DistrictId",
                table: "Wards");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Wards",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DistrictId",
                table: "Wards",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Wards",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wards_Districts_DistrictId",
                table: "Wards",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wards_Districts_DistrictId",
                table: "Wards");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Wards");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Wards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DistrictId",
                table: "Wards",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wards_Districts_DistrictId",
                table: "Wards",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
