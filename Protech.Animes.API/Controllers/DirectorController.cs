using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Protech.Animes.API.Models;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Application.UseCases.DirectorUseCases;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DirectorController : ControllerBase
{
    private readonly GetDirectorUseCase _getDirectorUseCase;
    private readonly GetDirectorsUseCase _getDirectorsUseCase;
    private readonly GetDirectorsByNameUseCase _getDirectorsByNameUseCase;
    private readonly CreateDirectorUseCase _createDirectorUseCase;
    private readonly UpdateDirectorUseCase _updateDirectorUseCase;
    private readonly DeleteDirectorUseCase _deleteDirectorUseCase;
    private readonly ILogger<DirectorController> _logger;

    public DirectorController(
        GetDirectorUseCase getDirectorUseCase,
        GetDirectorsUseCase getDirectorsUseCase,
        GetDirectorsByNameUseCase getDirectorsByNameUseCase,
        CreateDirectorUseCase createDirectorUseCase,
        UpdateDirectorUseCase updateDirectorUseCase,
        DeleteDirectorUseCase deleteDirectorUseCase,
        ILogger<DirectorController> logger
        )
    {
        _getDirectorUseCase = getDirectorUseCase;
        _getDirectorsUseCase = getDirectorsUseCase;
        _getDirectorsByNameUseCase = getDirectorsByNameUseCase;
        _createDirectorUseCase = createDirectorUseCase;
        _updateDirectorUseCase = updateDirectorUseCase;
        _deleteDirectorUseCase = deleteDirectorUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Get all directors
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DirectorDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetDirectors([FromQuery] int? page, [FromQuery] int? limit)
    {
        try
        {
            _logger.LogInformation("GetDirectors called");

            var directors = await _getDirectorsUseCase.Execute(page, limit);
            return Ok(directors);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid page or limit");

            var error = new { message = ex.Message };
            return BadRequest(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the directors");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get a director by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DirectorDto), 200)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetDirector(int id)
    {
        try
        {
            _logger.LogInformation($"GetDirector called with id {id}");

            var director = await _getDirectorUseCase.Execute(id);

            _logger.LogInformation($"Director with id {id} found");

            return Ok(director);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Director not found");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 404 };
            return NotFound(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the director");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get directors by name pattern
    /// </summary>
    [HttpGet("name/{name}")]
    [ProducesResponseType(typeof(IEnumerable<DirectorDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetDirectorsByName(string name, [FromQuery] int? page, [FromQuery] int? pageSize)
    {
        try
        {
            _logger.LogInformation($"GetDirectorByName called with name {name}");

            var director = await _getDirectorsByNameUseCase.Execute(name, page, pageSize);

            _logger.LogInformation($"Director with name {name} found");

            return Ok(director);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Director not found");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 404 };
            return NotFound(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the director");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Create a director
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DirectorDto), 201)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateDirector(CreateDirectorDto createDirectorDto)
    {
        try
        {
            _logger.LogInformation("CreateDirector called");

            var director = await _createDirectorUseCase.Execute(createDirectorDto);

            return CreatedAtAction(nameof(CreateDirector), new { id = director.Id }, director);
        }
        catch (DuplicatedEntityException ex)
        {
            _logger.LogWarning(ex, "Director already exists");

            var error = new { message = ex.Message };
            return BadRequest(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the director");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete a director by id
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(typeof(ErrorModel), 409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteDirector(int id)
    {
        try
        {
            _logger.LogInformation($"DeleteDirector called with id {id}");

            var deleted = await _deleteDirectorUseCase.Execute(id);

            if (deleted)
            {
                _logger.LogInformation($"Director with id {id} deleted");

                return NoContent();
            }

            _logger.LogWarning($"Director with id {id} could not be deleted");

            var error = new ErrorModel { Message = "Director not found", StatusCode = 404 };
            return NotFound(error);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Director has animes associated with it. Cannot delete.");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 409 };
            return Conflict(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the director");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update a director by id
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DirectorDto), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdateDirector(int id, UpdateDirectorDto updateDirectorDto)
    {
        try
        {
            _logger.LogInformation($"UpdateDirector called with id {id}");

            var director = await _updateDirectorUseCase.Execute(id, updateDirectorDto);

            _logger.LogInformation($"Director with id {id} updated");

            return Ok(director);
        }
        catch (BadRequestException ex)
        {
            _logger.LogWarning(ex, "Id does not match");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 400 };
            return BadRequest(error);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Director not found");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 404 };
            return NotFound(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the director");

            return StatusCode(500);
        }
    }
}