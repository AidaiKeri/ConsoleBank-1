using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleBank_1.Migrations
{
    public partial class BugFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comission",
                table: "HistoryOfTransactions",
                newName: "Commission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Commission",
                table: "HistoryOfTransactions",
                newName: "Comission");
        }
    }
}
