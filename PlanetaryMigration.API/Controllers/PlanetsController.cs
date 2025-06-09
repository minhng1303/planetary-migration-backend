using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanetaryMigration.Application.Interfaces;
using PlanetaryMigration.Application.Models;
using PlanetaryMigration.Domain.Entities;
using static PlanetaryMigration.Domain.Constants.Roles;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Require authentication for all actions
public class PlanetsController : ControllerBase
{
    private readonly IPlanetService _planetService;
    private readonly IEvaluationService _evaluationService;

    public PlanetsController(IPlanetService planetService, IEvaluationService evaluationService)
    {
        _planetService = planetService;
        _evaluationService = evaluationService;
    }

    [HttpGet]
    [Authorize(Roles = AllUser)]
    public IActionResult GetPlanets()
    {
        var planets = _planetService.GetAccessiblePlanets(User).ToList();
        return Ok(planets);
    }

    [HttpGet("get-planet-with-statistic")]
    [Authorize(Roles = AllUser)]
    public IActionResult GetPlanetsWithStatistic()
    {
        var planets = _evaluationService.EvaluatePlanets(User).ToList();
        return Ok(planets);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = AllUser)]
    public IActionResult GetPlanet(int id)
    {
        var planet = _planetService.GetPlanetById(id, User);
        return Ok(planet);
    }

    [HttpPost]
    [Authorize(Roles = SuperAdmin)]
    public IActionResult CreatePlanet([FromBody] CreatePlanetRequest request)
    {
        var planet = _planetService.CreatePlanet(request, User);
        return CreatedAtAction(nameof(GetPlanet), new { id = planet.Id }, planet);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Admins)]
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
    [Authorize(Roles = Admins)]
    public IActionResult DeletePlanet(int id)
    {
        try
        {
            _planetService.DeletePlanet(id, User);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

}
