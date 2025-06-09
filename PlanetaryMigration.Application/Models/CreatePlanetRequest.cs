using PlanetaryMigration.Domain.Entities;

namespace PlanetaryMigration.Application.Models
{
    public class CreatePlanetRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Factor> Factors { get; set; } = [];
    }
}
