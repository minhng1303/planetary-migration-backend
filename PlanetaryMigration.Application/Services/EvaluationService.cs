using PlanetaryMigration.Application.Interfaces;
using PlanetaryMigration.Domain.Entities;

namespace PlanetaryMigration.Application.Services;

public class EvaluationService : IEvaluationService
{
    public EvaluationResult EvaluatePlanets(IEnumerable<Planet> planets)
    {
        var results = new List<PlanetScore>();

        foreach (var planet in planets)
        {
            double score = 0;
            double totalWeight = 0;

            foreach (var factor in planet.Factors)
            {
                score += factor.Value * factor.Weight;
                totalWeight += factor.Weight;
            }

            results.Add(new PlanetScore
            {
                PlanetId = planet.Id,
                PlanetName = planet.Name,
                Score = totalWeight > 0 ? score / totalWeight : 0
            });
        }

        var bestPlanet = results.OrderByDescending(r => r.Score).First();

        return new EvaluationResult
        {
            PlanetScores = results,
            BestPlanetId = bestPlanet.PlanetId,
            BestPlanetName = bestPlanet.PlanetName,
            EvaluationDate = DateTime.UtcNow
        };
    }
}

public record PlanetScore
{
    public int PlanetId { get; init; }
    public string PlanetName { get; init; } = string.Empty;
    public double Score { get; init; }
}

public record EvaluationResult
{
    public List<PlanetScore> PlanetScores { get; init; } = new();
    public int BestPlanetId { get; init; }
    public string BestPlanetName { get; init; } = string.Empty;
    public DateTime EvaluationDate { get; init; }
}