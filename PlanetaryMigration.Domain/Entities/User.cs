using PlanetaryMigration.Domain.Enums;

namespace PlanetaryMigration.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public int? AssignedPlanetId { get; set; }
    public Planet? AssignedPlanet { get; set; }
}