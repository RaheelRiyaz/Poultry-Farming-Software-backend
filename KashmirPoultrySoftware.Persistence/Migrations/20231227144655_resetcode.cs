using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KashmirPoultrySoftware.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class resetcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResetCodeExpirationTime",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetCodeExpirationTime",
                table: "Users");
        }
    }
}
