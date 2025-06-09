namespace PlanetaryMigration.Domain.DTOs
{
    public class PlanetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double PlanetScore { get; set; }
        public string PlanetType { get; set; }
        public List<PlanetFactorDto> Factors { get; set; }
    }

}
