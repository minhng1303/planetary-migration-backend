using AutoMapper;
using PlanetaryMigration.Application.Interfaces;
using PlanetaryMigration.Domain.DTOs;

namespace PlanetaryMigration.Application.Services;

public class FactorService : IFactorService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FactorService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public IQueryable<FactorDto> GetFactors()
    {
        return _context.Factors
               .Select(p => new FactorDto
               {
                   Name = p.Name,
                   Unit = p.Unit,
                   Weight = p.Weight,
               });
    }
}

