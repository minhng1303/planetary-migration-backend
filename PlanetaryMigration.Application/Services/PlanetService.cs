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

        public PlanetService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Planet> GetAccessiblePlanets(ClaimsPrincipal user)
        {
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            var assignedId = int.Parse(user.FindFirst("AssignedPlanetId")?.Value ?? "0");

            IQueryable<Planet> query = _context.Planets.Include(p => p.PlanetFactors);

            return role switch
            {
                nameof(UserRole.PlanetAdmin) => query.Where(p => p.Id == assignedId),
                nameof(UserRole.ViewerType1) => query.Where(p => p.PlanetType == PlannetType.TYPE_1.ToString()),
                nameof(UserRole.ViewerType2) => query.Where(p => p.PlanetType == PlannetType.TYPE_2.ToString()),
                nameof(UserRole.SuperAdmin) => query,
                _ => Enumerable.Empty<Planet>().AsQueryable()
            };
        }

        public PlanetDto? GetPlanetById(int id, ClaimsPrincipal user)
        {
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            var assignedId = int.Parse(user.FindFirst("AssignedPlanetId")?.Value ?? "0");

            var query = _context.Planets
                .Where(p => p.Id == id)
                .Select(p => new PlanetDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    PlanetType = p.PlanetType,
                    Factors = p.PlanetFactors.Select(pf => new PlanetFactorDto
                    {
                        Id = pf.Id,
                        PlanetId = pf.PlanetId,
                        Name = pf.Factor.Name,
                        Value = pf.Value,
                        Unit = pf.Factor.Unit,
                        Weight = pf.Factor.Weight
                    }).ToList()
                });

            return role switch
            {
                nameof(UserRole.SuperAdmin) => query.FirstOrDefault(),
                nameof(UserRole.PlanetAdmin) => assignedId == id ? query.FirstOrDefault() : null,
                nameof(UserRole.ViewerType1) => query.Where(p => p.PlanetType == PlannetType.TYPE_1.ToString()).FirstOrDefault(),
                nameof(UserRole.ViewerType2) => query.Where(p => p.PlanetType == PlannetType.TYPE_2.ToString()).FirstOrDefault(),
                _ => null
            };
        }

        public Planet CreatePlanet(CreatePlanetRequest request, ClaimsPrincipal user)
        {
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            if (role != nameof(UserRole.SuperAdmin))
                throw new UnauthorizedAccessException();

            var newPlanet = new Planet
            {
                Name = request.Name,
                Description = request.Description,
                PlanetType = request.PlanetType,
                PlanetFactors = request.Factors.Select(f => new PlanetFactor
                {
                    FactorId = f.Id,
                    Value = f.Value
                }).ToList()
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

        public void DeletePlanet(int id, ClaimsPrincipal user)
        {
            var users = _context.Users.Where(u => u.AssignedPlanetId == id).ToList();
            foreach (var u in users)
            {
                u.AssignedPlanetId = null; // if nullable
            }
            _context.SaveChanges();

            var planet = _context.Planets.Find(id);
            if (planet != null)
            {
                _context.Planets.Remove(planet);
                _context.SaveChanges();
            }
        }

        public bool HasAccessToPlanet(int planetId, ClaimsPrincipal user)
        {
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            var assignedId = int.Parse(user.FindFirst("AssignedPlanetId")?.Value ?? "0");

            var planet = _context.Planets.Find(planetId);
            if (planet == null) return false;

            return role switch
            {
                nameof(UserRole.SuperAdmin) => true,
                nameof(UserRole.PlanetAdmin) => planetId == assignedId,
                nameof(UserRole.ViewerType1) => planet.PlanetType == PlannetType.TYPE_1.ToString(),
                nameof(UserRole.ViewerType2) => planet.PlanetType == PlannetType.TYPE_2.ToString(),
                _ => false
            };
        }
    }
}
