using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlanetaryMigration.Application.Interfaces;
using PlanetaryMigration.Application.Models;
using PlanetaryMigration.Domain.DTOs;
using PlanetaryMigration.Domain.Entities;
using PlanetaryMigration.Domain.Enums;
using System.Security.Claims;

namespace PlanetaryMigration.Application.Services
{
    public class PlanetService : IPlanetService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PlanetService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public IQueryable<Planet> GetAccessiblePlanets(ClaimsPrincipal user)
        {
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            var assignedId = int.Parse(user.FindFirst("AssignedPlanetId")?.Value ?? "0");

            IQueryable<Planet> query = _context.Planets.Include(p => p.PlanetFactors);

            return role switch
            {
                nameof(UserRole.PlanetAdmin) => query.Where(p => p.Id == assignedId),
                nameof(UserRole.ViewerType1) => query.Where(p => p.Id == 1),
                nameof(UserRole.ViewerType2) => query.Where(p => p.Id == 1 || p.Id == 3),
                _ => query
            };
        }

        public PlanetDto? GetPlanetById(int id)
        {
            return _context.Planets
                .Where(p => p.Id == id)
                .Select(p => new PlanetDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Factors = p.PlanetFactors.Select(pf => new PlanetFactorDto
                    {
                        Id = pf.Id,
                        PlanetId = pf.PlanetId,
                        Name = pf.Factor.Name,
                        Value = pf.Value,
                        Unit = pf.Factor.Unit,
                        Weight = pf.Factor.Weight
                    }).ToList()
                })
                .FirstOrDefault();
        }

        public Planet CreatePlanet(CreatePlanetRequest planet)
        {
            var factors = _mapper.Map<List<PlanetFactor>>(planet.Factors);

            var newPlanet = new Planet
            {
                Name = planet.Name,
                Description = planet.Description,
                PlanetFactors = factors
            };
            _context.Planets.Add(newPlanet);
            _context.SaveChanges();
            return newPlanet;
        }

        public void UpdatePlanet(int id, Planet planet, ClaimsPrincipal user)
        {
            if (id != planet.Id)
                throw new ArgumentException("ID mismatch");

            if (!HasAccessToPlanet(id, user))
                throw new UnauthorizedAccessException();

            _context.Entry(planet).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletePlanet(int id)
        {
            var planet = _context.Planets.Find(id);
            if (planet != null)
            {
                _context.Planets.Remove(planet);
                _context.SaveChanges();
            }
        }

        public PlanetFactor AddFactorToPlanet(int planetId, PlanetFactor factor, ClaimsPrincipal user)
        {
            if (!HasAccessToPlanet(planetId, user))
                throw new UnauthorizedAccessException();

            factor.PlanetId = planetId;
            _context.PlanetFactors.Add(factor);
            _context.SaveChanges();
            return factor;
        }

        public bool HasAccessToPlanet(int planetId, ClaimsPrincipal user)
        {
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            var assignedId = int.Parse(user.FindFirst("AssignedPlanetId")?.Value ?? "0");

            return role switch
            {
                nameof(UserRole.SuperAdmin) => true,
                nameof(UserRole.PlanetAdmin) => planetId == assignedId,
                nameof(UserRole.ViewerType1) => planetId == 1,
                nameof(UserRole.ViewerType2) => planetId == 1 || planetId == 3,
                _ => false
            };
        }
    }
}
