using AutoMapper;
using PlanetaryMigration.Application.Interfaces;
using PlanetaryMigration.Domain.DTOs;

namespace PlanetaryMigration.Application.Services;

public class EvaluationService : IEvaluationService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public EvaluationService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IQueryable<PlanetDto> EvaluatePlanets()
    {
        var query = _context.Planets
                .Where(x => x.PlanetFactors.Count > 0)
                .Take(3)
                .Select(p => new PlanetDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    PlanetScore = p.PlanetFactors.Sum(x => x.Value * x.Factor.Weight) / p.PlanetFactors.Count,
                    Factors = p.PlanetFactors.Select(x => new PlanetFactorDto()
                    {
                        Id = x.Id,
                        PlanetId = x.PlanetId,
                        Name = x.Factor.Name,
                        Value = x.Value,
                        Unit = x.Factor.Unit,
                        Weight = x.Factor.Weight
                    }).ToList()
                }).OrderBy(x => x.PlanetScore);


        return query;
    }
}

