using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftUniFest.Data.Migrations
{
    public partial class ModifyPosTerminalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POSTerminals_Employees_EmployeeId",
                table: "POSTerminals");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "POSTerminals",
                newName: "TraderId");

            migrationBuilder.RenameIndex(
                name: "IX_POSTerminals_EmployeeId",
                table: "POSTerminals",
                newName: "IX_POSTerminals_TraderId");

            migrationBuilder.AddForeignKey(
                name: "FK_POSTerminals_Traders_TraderId",
                table: "POSTerminals",
                column: "TraderId",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POSTerminals_Traders_TraderId",
                table: "POSTerminals");

            migrationBuilder.RenameColumn(
                name: "TraderId",
                table: "POSTerminals",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_POSTerminals_TraderId",
                table: "POSTerminals",
                newName: "IX_POSTerminals_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_POSTerminals_Employees_EmployeeId",
                table: "POSTerminals",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
