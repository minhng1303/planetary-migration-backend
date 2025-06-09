using Microsoft.EntityFrameworkCore.Migrations;
using PlanetaryMigration.Domain.Enums;

namespace PlanetaryMigration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
             table: "Users",
             columns: new[] { "Id", "Username", "PasswordHash", "Role", "AssignedPlanetId" },
             values: new object[,]
             {
                    { 1, "superadmin", BCrypt.Net.BCrypt.HashPassword("123456"), (int)UserRole.SuperAdmin, null },
                    { 2, "planetadmin1", BCrypt.Net.BCrypt.HashPassword("admin123"), (int)UserRole.PlanetAdmin, 1 },  
                    { 3, "viewer1", BCrypt.Net.BCrypt.HashPassword("admin123"), (int)UserRole.ViewerType1, null },
                    { 4, "viewer2", BCrypt.Net.BCrypt.HashPassword("admin123"), (int)UserRole.ViewerType2, null }
             });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });
        }

    }
}
