using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetaryMigration.Domain.Entities
{
    [Table("Factors")]
    public class Factor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public double Weight { get; set; }
        public ICollection<PlanetFactor> PlanetFactors { get; set; } = new List<PlanetFactor>();
    }

}
