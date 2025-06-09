using PlanetaryMigration.Application.Services;
using PlanetaryMigration.Domain.DTOs;
using PlanetaryMigration.Domain.Entities;

namespace PlanetaryMigration.Application.Interfaces;

public interface IEvaluationService
{
    IQueryable<PlanetDto> EvaluatePlanets();
}