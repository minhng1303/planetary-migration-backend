using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlanetaryMigration.Application.Interfaces;
using PlanetaryMigration.Domain.DTOs;
using PlanetaryMigration.Domain.Entities;
using PlanetaryMigration.Domain.Enums;
using System.Security.Claims;

namespace PlanetaryMigration.Application.Services;

public class EvaluationService : IEvaluationService
{
    private readonly AppDbContext _context;

    public EvaluationService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<PlanetDto> EvaluatePlanets(ClaimsPrincipal user)
    {
        var role = user.FindFirst(ClaimTypes.Role)?.Value;
        var assignedId = int.Parse(user.FindFirst("AssignedPlanetId")?.Value ?? "0");

        var query = _context.Planets
            .Include(p => p.PlanetFactors)
                .ThenInclude(pf => pf.Factor)
            .Where(p => p.PlanetFactors.Count > 0);

        // Filter based on role
        query = role switch
        {
            nameof(UserRole.SuperAdmin) => query,
            nameof(UserRole.PlanetAdmin) => query.Where(p => p.Id == assignedId),
            nameof(UserRole.ViewerType1) => query.Where(p => p.PlanetType == PlannetType.TYPE_1.ToString()),
            nameof(UserRole.ViewerType2) => query.Where(p => p.PlanetType == PlannetType.TYPE_2.ToString()),
            _ => Enumerable.Empty<Planet>().AsQueryable()
        };

        // Map to DTO and evaluate score
        var result = query
            .Take(3)
            .Select(p => new PlanetDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                PlanetScore = p.PlanetFactors.Sum(x => x.Value * x.Factor.Weight) / p.PlanetFactors.Count,
                Factors = p.PlanetFactors.Select(x => new PlanetFactorDto
                {
                    Id = x.Id,
                    PlanetId = x.PlanetId,
                    Name = x.Factor.Name,
                    Value = x.Value,
                    Unit = x.Factor.Unit,
                    Weight = x.Factor.Weight
                }).ToList()
            })
            .OrderBy(x => x.PlanetScore);

        return result;
    }
}
