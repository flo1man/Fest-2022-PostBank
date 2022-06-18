using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftUniFest.Data.Migrations.ApplicationDbContext2Migrations
{
    public partial class AddApproveCountToDiscounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApproveCount",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveCount",
                table: "Discounts");
        }
    }
}
