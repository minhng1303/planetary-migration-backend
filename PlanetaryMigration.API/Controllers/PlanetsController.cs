// PlanetaryMigration.API/Controllers/PlanetsController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanetaryMigration.Application.Interfaces;
using PlanetaryMigration.Application.Models;
using PlanetaryMigration.Domain.Entities;
using PlanetaryMigration.Domain.Enums;
using System;
using System.Numerics;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
//[Authorize]]
public class PlanetsController : ControllerBase
{
    private readonly IPlanetService _planetService;

    public PlanetsController(IPlanetService planetService)
    {
        _planetService = planetService;
    }

    [HttpGet]
    public IActionResult GetPlanets()
    {
        var planets = _planetService.GetAccessiblePlanets(User).ToList();
        return Ok(planets);
    }

    [HttpGet("{id}")]
    public IActionResult GetPlanet(int id)
    {
        var planet = _planetService.GetPlanetById(id);
        return Ok(planet);
    }

    [HttpPost]
    public IActionResult CreatePlanet([FromBody] CreatePlanetRequest request)
    {
        var planet = _planetService.CreatePlanet(request);
        return CreatedAtAction(nameof(GetPlanet), new { id = planet.Id }, planet);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePlanet(int id, [FromBody] Planet planet)
    {
        try
        {
            _planetService.UpdatePlanet(id, planet, User);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePlanet(int id)
    {
        _planetService.DeletePlanet(id);
        return NoContent();
    }

    [HttpPost("{id}/factors")]
    public IActionResult AddFactor(int id, [FromBody] PlanetFactor factor)
    {
        try
        {
            var added = _planetService.AddFactorToPlanet(id, factor, User);
            return CreatedAtAction(nameof(GetPlanet), new { id }, added);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}


