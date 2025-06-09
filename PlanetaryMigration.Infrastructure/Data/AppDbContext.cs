using Microsoft.EntityFrameworkCore;
using PlanetaryMigration.Domain.Entities;
using PlanetaryMigration.Domain.Enums;
using System.Reflection.Emit;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Planet> Planets => Set<Planet>();
    public DbSet<Factor> Factors => Set<Factor>();
    public DbSet<PlanetFactor> PlanetFactors => Set<PlanetFactor>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<PlanetFactor>()
        .HasIndex(pf => new { pf.PlanetId, pf.FactorId })
        .IsUnique();

        builder.Entity<PlanetFactor>()
            .HasOne(pf => pf.Planet)
            .WithMany(p => p.PlanetFactors)
            .HasForeignKey(pf => pf.PlanetId);

        builder.Entity<PlanetFactor>()
            .HasOne(pf => pf.Factor)
            .WithMany(f => f.PlanetFactors)
            .HasForeignKey(pf => pf.FactorId);


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