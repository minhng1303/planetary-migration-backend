using System.Text.Json.Serialization;

namespace PlanetaryMigration.Domain.Entities;

public class PlanetFactor
{
    public int Id { get; set; }

    public int PlanetId { get; set; }
    [JsonIgnore]
    public Planet? Planet { get; set; }

    public string Name { get; set; } = string.Empty;

    public double Value { get; set; }

    public string? Unit { get; set; }  

    public double Weight { get; set; }
}
