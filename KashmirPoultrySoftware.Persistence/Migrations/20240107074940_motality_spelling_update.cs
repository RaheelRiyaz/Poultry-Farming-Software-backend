using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirPoultrySoftware.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class motality_spelling_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalMotivality",
                table: "Hatches",
                newName: "TotalMotality");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalMotality",
                table: "Hatches",
                newName: "TotalMotivality");
        }
    }
}
