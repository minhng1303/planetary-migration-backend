using PlanetaryMigration.Application.Models;
using PlanetaryMigration.Domain.DTOs;

namespace PlanetaryMigration.Application.Interfaces
{
    public interface IFactorService
    {
        IQueryable<FactorDto> GetFactors();
    }
}
