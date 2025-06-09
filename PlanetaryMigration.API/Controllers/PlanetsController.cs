// PlanetaryMigration.API/Controllers/PlanetsController.cs
using Microsoft.AspNetCore.Mvc;
using PlanetaryMigration.Application.Interfaces;
using PlanetaryMigration.Application.Models;
using PlanetaryMigration.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
//[Authorize]]
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
    public IActionResult GetPlanets()
    {
        var planets = _planetService.GetAccessiblePlanets(User).ToList();
        return Ok(planets);
    }

    [HttpGet("get-planet-with-statistic")]
    public IActionResult GetPlanetsWithStatistic()
    {
        var planets = _evaluationService.EvaluatePlanets().ToList();
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
}


