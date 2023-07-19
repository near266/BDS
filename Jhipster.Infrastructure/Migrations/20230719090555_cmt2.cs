using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class cmt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLike",
                table: "Comment",
                newName: "LikeCount");

            migrationBuilder.AddColumn<List<string>>(
                name: "Rely",
                table: "Comment",
                type: "text[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rely",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "LikeCount",
                table: "Comment",
                newName: "IsLike");
        }
    }
}
