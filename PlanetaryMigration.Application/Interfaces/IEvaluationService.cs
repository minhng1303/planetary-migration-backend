using PlanetaryMigration.Application.Services;
using PlanetaryMigration.Domain.DTOs;
using PlanetaryMigration.Domain.Entities;
using System.Security.Claims;

namespace PlanetaryMigration.Application.Interfaces;

public interface IEvaluationService
{
    IQueryable<PlanetDto> EvaluatePlanets(ClaimsPrincipal user);
}