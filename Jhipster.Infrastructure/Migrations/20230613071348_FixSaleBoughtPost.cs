using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class FixSaleBoughtPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "SalePosts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangeDate",
                table: "SalePosts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "BoughtPosts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangeDate",
                table: "BoughtPosts",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "SalePosts");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                table: "SalePosts");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "BoughtPosts");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                table: "BoughtPosts");
        }
    }
}
