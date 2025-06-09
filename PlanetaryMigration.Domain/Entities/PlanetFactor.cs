using System.Text.Json.Serialization;

namespace PlanetaryMigration.Domain.Entities;

public class PlanetFactor
{
    public int Id { get; set; }

    public int PlanetId { get; set; }
    [JsonIgnore]
    public Planet Planet { get; set; } = null!;

    public int FactorId { get; set; }
    [JsonIgnore]
    public Factor Factor { get; set; } = null!;

    public double Value { get; set; }
}

