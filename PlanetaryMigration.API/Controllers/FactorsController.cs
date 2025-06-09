using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanetaryMigration.Application.Interfaces;
using PlanetaryMigration.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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

    [HttpGet("{id}")]
    public IActionResult GetFactor(int id)
    {
        var factor = _factorService.GetFactorById(id);
        if (factor == null) return NotFound();
        return Ok(factor);
    }

    [HttpPost]
    public IActionResult CreateFactor([FromBody] Factor factor)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var createdFactor = _factorService.CreateFactor(factor);
        return CreatedAtAction(nameof(GetFactor), new { id = createdFactor.Id }, createdFactor);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFactor(int id, [FromBody] Factor factor)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updated = _factorService.UpdateFactor(id, factor);
        if (!updated) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFactor(int id)
    {
        var deleted = _factorService.DeleteFactor(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
