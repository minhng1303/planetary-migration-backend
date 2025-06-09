using PlanetaryMigration.Application.Models;
using PlanetaryMigration.Domain.DTOs;
using PlanetaryMigration.Domain.Entities;
using System.Security.Claims;

namespace PlanetaryMigration.Application.Interfaces
{
    public interface IPlanetService
    {
        IQueryable<Planet> GetAccessiblePlanets(ClaimsPrincipal user);
        PlanetDto? GetPlanetById(int id, ClaimsPrincipal user);
        Planet CreatePlanet(CreatePlanetRequest planet, ClaimsPrincipal user);
        void UpdatePlanet(int id, Planet planet, ClaimsPrincipal user);
        void DeletePlanet(int id, ClaimsPrincipal user);
        bool HasAccessToPlanet(int planetId, ClaimsPrincipal user);
    }
}
