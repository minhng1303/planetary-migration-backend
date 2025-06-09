using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetaryMigration.Domain.Entities;

[Table("Planets")]
public class Planet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PlanetType { get; set; } = string.Empty;
    public ICollection<PlanetFactor> PlanetFactors { get; set; } = new List<PlanetFactor>();
}