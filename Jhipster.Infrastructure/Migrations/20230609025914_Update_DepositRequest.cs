using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class Update_DepositRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DepositRequests_CustomerId",
                table: "DepositRequests",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositRequests_Customers_CustomerId",
                table: "DepositRequests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepositRequests_Customers_CustomerId",
                table: "DepositRequests");

            migrationBuilder.DropIndex(
                name: "IX_DepositRequests_CustomerId",
                table: "DepositRequests");
        }
    }
}
