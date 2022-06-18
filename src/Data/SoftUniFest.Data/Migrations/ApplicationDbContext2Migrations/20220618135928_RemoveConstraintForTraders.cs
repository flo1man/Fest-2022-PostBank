using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftUniFest.Data.Migrations.ApplicationDbContext2Migrations
{
    public partial class RemoveConstraintForTraders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Traders_TraderId",
                table: "Discounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Traders_TraderId",
                table: "Discounts",
                column: "TraderId",
                principalTable: "Traders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Traders_TraderId",
                table: "Discounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Traders_TraderId",
                table: "Discounts",
                column: "TraderId",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
