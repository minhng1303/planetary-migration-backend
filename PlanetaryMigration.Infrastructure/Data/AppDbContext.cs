using Microsoft.EntityFrameworkCore;
using PlanetaryMigration.Domain.Entities;
using PlanetaryMigration.Domain.Enums;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Planet> Planets => Set<Planet>();
    public DbSet<PlanetFactor> PlanetFactors => Set<PlanetFactor>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Planet>().HasData(
            new Planet { Id = 1, Name = "Proxima Centauri b" },
            new Planet { Id = 2, Name = "TRAPPIST-1e" },
            new Planet { Id = 3, Name = "Kepler-442b" }
        );

        builder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "superadmin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = UserRole.SuperAdmin
            },
            new User
            {
                Id = 2,
                Username = "admin1",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = UserRole.PlanetAdmin,
                AssignedPlanetId = 1
            }
        );
    }
}