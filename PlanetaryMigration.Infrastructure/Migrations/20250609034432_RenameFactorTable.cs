using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanetaryMigration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameFactorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Factor",
                newName: "Factors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Factors",
                newName: "Factor");
        }
    }
}
