using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftUniFest.Data.Migrations
{
    public partial class AddCardInfoToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditCardNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpiredOn",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCardNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExpiredOn",
                table: "AspNetUsers");
        }
    }
}
