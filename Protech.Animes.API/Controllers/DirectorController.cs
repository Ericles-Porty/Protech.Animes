using Microsoft.AspNetCore.Mvc;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;

namespace Protech.Animes.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DirectorController : ControllerBase
{

    private readonly IDirectorService _directorService;
    private readonly ILogger<DirectorController> _logger;

    public DirectorController(IDirectorService directorService, ILogger<DirectorController> logger)
    {
        _logger = logger;
        _directorService = directorService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DirectorDto>), 200)]
    public async Task<IActionResult> GetDirectors()
    {
        var directors = await _directorService.GetDirectors();

        _logger.LogInformation("GetDirectors called");

        return Ok(directors);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DirectorDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetDirector(int id)
    {
        var director = await _directorService.GetDirector(id);

        _logger.LogInformation($"GetDirector called with id {id}");

        if (director == null)
        {
            _logger.LogWarning($"Director with id {id} not found");

            return NotFound();
        }

        return Ok(director);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DirectorDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateDirector(DirectorDto directorDto)
    {
        var director = await _directorService.CreateDirector(directorDto);

        _logger.LogInformation("CreateDirector called");

        return CreatedAtAction(nameof(CreateDirector), new { id = director.Id }, director);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteDirector(int id)
    {
        var deleted = await _directorService.DeleteDirector(id);

        _logger.LogInformation($"DeleteDirector called with id {id}");

        if (deleted)
        {
            _logger.LogInformation($"Director with id {id} deleted");

            return NoContent();
        }

        _logger.LogWarning($"Director with id {id} could not be deleted");

        return NotFound();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DirectorDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateDirector(int id, DirectorDto directorDto)
    {
        var director = await _directorService.UpdateDirector(id, directorDto);

        _logger.LogInformation($"UpdateDirector called with id {id}");

        return Ok(director);
    }
}