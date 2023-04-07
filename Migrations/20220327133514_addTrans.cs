using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleBank_1.Migrations
{
    public partial class addTrans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryOfTransaction_Persons_ClientId",
                table: "HistoryOfTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryOfTransaction",
                table: "HistoryOfTransaction");

            migrationBuilder.RenameTable(
                name: "HistoryOfTransaction",
                newName: "HistoryOfTransactions");

            migrationBuilder.RenameColumn(
                name: "ReportDateTime",
                table: "HistoryOfTransactions",
                newName: "ReportDate");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryOfTransaction_ClientId",
                table: "HistoryOfTransactions",
                newName: "IX_HistoryOfTransactions_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryOfTransactions",
                table: "HistoryOfTransactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryOfTransactions_Persons_ClientId",
                table: "HistoryOfTransactions",
                column: "ClientId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryOfTransactions_Persons_ClientId",
                table: "HistoryOfTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryOfTransactions",
                table: "HistoryOfTransactions");

            migrationBuilder.RenameTable(
                name: "HistoryOfTransactions",
                newName: "HistoryOfTransaction");

            migrationBuilder.RenameColumn(
                name: "ReportDate",
                table: "HistoryOfTransaction",
                newName: "ReportDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryOfTransactions_ClientId",
                table: "HistoryOfTransaction",
                newName: "IX_HistoryOfTransaction_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryOfTransaction",
                table: "HistoryOfTransaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryOfTransaction_Persons_ClientId",
                table: "HistoryOfTransaction",
                column: "ClientId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
