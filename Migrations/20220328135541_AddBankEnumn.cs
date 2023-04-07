using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleBank_1.Migrations
{
    public partial class AddBankEnumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bank",
                table: "Persons",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank",
                table: "Persons");
        }
    }
}
