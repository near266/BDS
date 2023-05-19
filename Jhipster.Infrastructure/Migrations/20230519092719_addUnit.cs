using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class addUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "BoughtPosts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "BoughtPosts");
        }
    }
}
