namespace PlanetaryMigration.Domain.Entities;

public class Planet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<PlanetFactor> Factors { get; set; } = new List<PlanetFactor>();
}