using PlanetaryMigration.Application.Services;
using PlanetaryMigration.Domain.Entities;

namespace PlanetaryMigration.Application.Interfaces;

public interface IEvaluationService
{
    EvaluationResult EvaluatePlanets(IEnumerable<Planet> planets);
}