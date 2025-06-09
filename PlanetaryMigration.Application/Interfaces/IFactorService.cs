using PlanetaryMigration.Domain.Entities;

namespace PlanetaryMigration.Application.Interfaces
{
    public interface IFactorService
    {
            IQueryable<Factor> GetFactors();
            Factor? GetFactorById(int id);
            Factor CreateFactor(Factor factor);
            bool UpdateFactor(int id, Factor factor);
            bool DeleteFactor(int id);
    }
}
