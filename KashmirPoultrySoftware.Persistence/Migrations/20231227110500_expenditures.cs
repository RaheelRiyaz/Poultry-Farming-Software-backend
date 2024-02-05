using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirPoultrySoftware.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class expenditures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenditure_Hatches_HatchId",
                table: "Expenditure");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenditure",
                table: "Expenditure");

            migrationBuilder.RenameTable(
                name: "Expenditure",
                newName: "Expenditures");

            migrationBuilder.RenameIndex(
                name: "IX_Expenditure_HatchId",
                table: "Expenditures",
                newName: "IX_Expenditures_HatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenditures",
                table: "Expenditures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenditures_Hatches_HatchId",
                table: "Expenditures",
                column: "HatchId",
                principalTable: "Hatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenditures_Hatches_HatchId",
                table: "Expenditures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenditures",
                table: "Expenditures");

            migrationBuilder.RenameTable(
                name: "Expenditures",
                newName: "Expenditure");

            migrationBuilder.RenameIndex(
                name: "IX_Expenditures_HatchId",
                table: "Expenditure",
                newName: "IX_Expenditure_HatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenditure",
                table: "Expenditure",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenditure_Hatches_HatchId",
                table: "Expenditure",
                column: "HatchId",
                principalTable: "Hatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
