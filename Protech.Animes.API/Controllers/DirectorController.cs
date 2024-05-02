using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Protech.Animes.API.Models;
using Protech.Animes.Application.CQRS.Commands.DirectorCommands;
using Protech.Animes.Application.CQRS.Queries;
using Protech.Animes.Application.CQRS.Queries.DirectorQueries;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DirectorController : ControllerBase
{
    private readonly ILogger<DirectorController> _logger;
    private readonly IMediator _mediator;

    public DirectorController(
        ILogger<DirectorController> logger,
        IMediator mediator
        )
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Get all directors
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DirectorDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetDirectors([FromQuery] PaginationParams paginationParams)
    {
        try
        {
            _logger.LogInformation("GetDirectors called");

            var directors = await _mediator.Send(new GetDirectorsQuery(paginationParams));
            return Ok(directors);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid page or pageSize");

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
    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType(typeof(DirectorDto), 200)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetDirector([FromRoute] int id)
    {
        try
        {
            _logger.LogInformation("GetDirector called");

            var director = await _mediator.Send(new GetDirectorByIdQuery(id));

            _logger.LogInformation("Director found");

            return Ok(director);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid id");

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
    public async Task<IActionResult> GetDirectorsByName([FromRoute] string name, [FromQuery] PaginationParams paginationParams)
    {
        try
        {
            _logger.LogInformation("GetDirectorByName called");

            var director = await _mediator.Send(new GetDirectorsByNameQuery(name, paginationParams));

            _logger.LogInformation("Director found");

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
    public async Task<IActionResult> CreateDirector([FromBody] CreateDirectorCommand createDirectorCommand)
    {
        try
        {
            _logger.LogInformation("CreateDirector called");

            var director = await _mediator.Send(createDirectorCommand);

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
    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(typeof(ErrorModel), 409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteDirector([FromRoute] int id)
    {
        try
        {
            _logger.LogInformation("DeleteDirector called");

            var deleted = await _mediator.Send(new DeleteDirectorCommand(id));
            if (deleted)
            {
                _logger.LogInformation("Director deleted");

                return NoContent();
            }

            _logger.LogWarning("Director could not be deleted");

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
    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(typeof(DirectorDto), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdateDirector([FromRoute] int id, [FromBody] UpdateDirectorCommand updateDirectorCommand)
    {
        try
        {
            _logger.LogInformation("UpdateDirector called");

            if (id != updateDirectorCommand.Id)
                throw new BadRequestException("Id does not match");

            var director = await _mediator.Send(updateDirectorCommand);

            _logger.LogInformation("Director updated");

            return Ok(director);
        }
        catch (BadRequestException ex)
        {
            _logger.LogWarning(ex, "Bad request exception occurred while updating the director");

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