namespace PlanetaryMigration.Domain.Entities;

public class Factor
{
    public int Id { get; set; }

    public int PlanetId { get; set; }

    public string Name { get; set; } = string.Empty;

    public double Value { get; set; }

    public string? Unit { get; set; }  

    public double Weight { get; set; }
}
