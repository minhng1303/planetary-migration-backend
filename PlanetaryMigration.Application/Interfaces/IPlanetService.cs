using PlanetaryMigration.Application.Models;
using PlanetaryMigration.Domain.Entities;
using System.Security.Claims;

namespace PlanetaryMigration.Application.Interfaces
{
    public interface IPlanetService
    {
        IQueryable<Planet> GetAccessiblePlanets(ClaimsPrincipal user);
        Planet? GetPlanetById(int id);
        Planet CreatePlanet(CreatePlanetRequest planet);
        void UpdatePlanet(int id, Planet planet, ClaimsPrincipal user);
        void DeletePlanet(int id);
        PlanetFactor AddFactorToPlanet(int planetId, PlanetFactor factor, ClaimsPrincipal user);
        bool HasAccessToPlanet(int planetId, ClaimsPrincipal user);
    }
}
