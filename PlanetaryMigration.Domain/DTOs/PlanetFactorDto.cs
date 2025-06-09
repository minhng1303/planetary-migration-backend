namespace PlanetaryMigration.Domain.DTOs
{
    public class PlanetFactorDto
    {
        public int? Id { get; set; }
        public int? PlanetId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public double Weight { get; set; }
    }

}
