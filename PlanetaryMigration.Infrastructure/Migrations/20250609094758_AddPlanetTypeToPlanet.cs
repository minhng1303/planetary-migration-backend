using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanetaryMigration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanetTypeToPlanet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanetFactors_Factor_FactorId",
                table: "PlanetFactors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Factor",
                table: "Factors");


            migrationBuilder.AddColumn<string>(
                name: "PlanetType",
                table: "Planets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Factors",
                table: "Factors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanetFactors_Factors_FactorId",
                table: "PlanetFactors",
                column: "FactorId",
                principalTable: "Factors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanetFactors_Factors_FactorId",
                table: "PlanetFactors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Factors",
                table: "Factors");

            migrationBuilder.DropColumn(
                name: "PlanetType",
                table: "Planets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Factor",
                table: "Factor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanetFactors_Factor_FactorId",
                table: "PlanetFactors",
                column: "FactorId",
                principalTable: "Factor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
