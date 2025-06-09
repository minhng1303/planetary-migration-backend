// PlanetaryMigration.API/Controllers/PlanetsController.cs
using Microsoft.AspNetCore.Mvc;
using PlanetaryMigration.Application.Interfaces;
using PlanetaryMigration.Application.Models;
using PlanetaryMigration.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
//[Authorize]]
public class FactorsController : ControllerBase
{
    private readonly IFactorService _factorService;

    public FactorsController(IFactorService factorService)
    {
        _factorService = factorService;
    }

    [HttpGet]
    public IActionResult GetFactors()
    {
        var factors = _factorService.GetFactors().ToList();
        return Ok(factors);
    }

    //[HttpPost("{id}/factors")]
    //public IActionResult AddFactor(int id, [FromBody] PlanetFactor factor)
    //{
    //    try
    //    {
    //        var added = _planetService.AddFactorToPlanet(id, factor, User);
    //        return CreatedAtAction(nameof(GetPlanet), new { id }, added);
    //    }
    //    catch (UnauthorizedAccessException)
    //    {
    //        return Forbid();
    //    }
    //}
}


