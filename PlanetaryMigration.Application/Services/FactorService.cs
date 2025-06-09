using Microsoft.EntityFrameworkCore;
using PlanetaryMigration.Application.Interfaces;
using PlanetaryMigration.Domain.Entities;

namespace PlanetaryMigration.Application.Services;

public class FactorService : IFactorService
{
    private readonly AppDbContext _context;

    public FactorService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Factor> GetFactors()
    {
        return _context.Factors.AsNoTracking();
    }

    public Factor? GetFactorById(int id)
    {
        return _context.Factors.Find(id);
    }

    public Factor CreateFactor(Factor factor)
    {
        _context.Factors.Add(factor);
        _context.SaveChanges();
        return factor;
    }

    public bool UpdateFactor(int id, Factor factor)
    {
        var existing = _context.Factors.Find(id);
        if (existing == null) return false;

        existing.Name = factor.Name;
        existing.Unit = factor.Unit;
        existing.Weight = factor.Weight;

        _context.SaveChanges();
        return true;
    }

    public bool DeleteFactor(int id)
    {
        var factor = _context.Factors.Find(id);
        if (factor == null) return false;

        _context.Factors.Remove(factor);
        _context.SaveChanges();
        return true;
    }
}


