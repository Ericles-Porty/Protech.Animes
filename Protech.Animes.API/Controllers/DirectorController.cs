using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Domain.Exceptions;

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
    [Authorize]
    public async Task<IActionResult> GetDirectors()
    {
        try
        {
            _logger.LogInformation("GetDirectors called");

            var directors = await _directorService.GetDirectors();

            return Ok(directors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the directors");

            return StatusCode(500);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DirectorDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetDirector(int id)
    {
        try
        {
            _logger.LogInformation($"GetDirector called with id {id}");

            var director = await _directorService.GetDirector(id);

            _logger.LogInformation($"Director with id {id} found");

            return Ok(director);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Director not found");

            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the director");

            return StatusCode(500);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(DirectorDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateDirector(DirectorDto directorDto)
    {
        try
        {
            _logger.LogInformation("CreateDirector called");

            var director = await _directorService.CreateDirector(directorDto);

            return CreatedAtAction(nameof(CreateDirector), new { id = director.Id }, director);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the director");

            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteDirector(int id)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the director");

            return StatusCode(500);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DirectorDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateDirector(int id, DirectorDto directorDto)
    {
        try
        {
            _logger.LogInformation($"UpdateDirector called with id {id}");

            var director = await _directorService.UpdateDirector(id, directorDto);

            _logger.LogInformation($"Director with id {id} updated");

            return Ok(director);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Director not found");

            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the director");

            return StatusCode(500);
        }
    }
}