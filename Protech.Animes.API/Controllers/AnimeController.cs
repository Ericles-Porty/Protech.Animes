using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Protech.Animes.API.Models;
using Protech.Animes.Application.CQRS.Commands.AnimeCommands;
using Protech.Animes.Application.CQRS.Queries;
using Protech.Animes.Application.CQRS.Queries.AnimesQueries;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly ILogger<AnimeController> _logger;
    private readonly IMediator _mediator;

    public AnimeController(
        ILogger<AnimeController> logger,
        IMediator mediator
        )
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Get all animes
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAnimes([FromQuery] PaginationParams paginationParams)
    {
        try
        {
            _logger.LogInformation("GetAnimes called");

            var animes = await _mediator.Send(new GetAnimesQuery(paginationParams));
            return Ok(animes);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "An error occurred while getting the animes");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 400 };
            return BadRequest(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the animes");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get an anime by id
    /// </summary>
    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType(typeof(AnimeDto), 200)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAnime([FromRoute] int id)
    {
        try
        {
            _logger.LogInformation("GetAnime called");

            var anime = await _mediator.Send(new GetAnimeByIdQuery(id));

            _logger.LogInformation("Anime found");

            return Ok(anime);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "An error occurred while getting the anime");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 400 };
            return BadRequest(error);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Anime not found");

            var error = new ErrorModel { Message = "Anime not found", StatusCode = 404 };
            return NotFound(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the anime");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Create an anime
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AnimeDto), 201)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateAnime([FromBody] CreateAnimeCommand createAnimeCommand)
    {
        try
        {
            _logger.LogInformation("CreateAnime called");

            var createdAnime = await _mediator.Send(createAnimeCommand);

            _logger.LogInformation("Anime created");

            return CreatedAtAction(nameof(CreateAnime), new { id = createdAnime.Id }, createdAnime);
        }
        catch (DuplicatedEntityException ex)
        {
            _logger.LogWarning(ex, "An error occurred while creating the anime");

            var error = new ErrorModel { Message = "Anime already exists", StatusCode = 400 };
            return BadRequest(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the anime");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update an anime
    /// </summary>
    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(typeof(UpdateAnimeCommand), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdateAnime([FromRoute] int id, [FromBody] UpdateAnimeCommand updateAnimeCommand)
    {
        try
        {
            _logger.LogInformation("UpdateAnime called");

            if (id != updateAnimeCommand.Id)
                throw new BadRequestException("Id in the body does not match the id in the route");

            var updatedAnime = await _mediator.Send(updateAnimeCommand);

            _logger.LogInformation("Anime updated");

            return Ok(updatedAnime);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Anime not found");

            var error = new ErrorModel { Message = "Anime not found", StatusCode = 404 };
            return NotFound(error);
        }
        catch (BadRequestException ex)
        {
            _logger.LogWarning(ex, "An error occurred while updating the anime");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 400 };
            return BadRequest(error);
        }
        catch (DuplicatedEntityException ex)
        {
            _logger.LogWarning(ex, "An error occurred while updating the anime");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 400 };
            return BadRequest(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the anime");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete an anime by id
    /// </summary>
    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteAnime([FromRoute] int id)
    {
        try
        {
            _logger.LogInformation("DeleteAnime called");

            var deleted = await _mediator.Send(new DeleteAnimeCommand(id));
            if (deleted is true)
            {
                _logger.LogInformation("Anime deleted");

                return NoContent();
            }

            _logger.LogWarning("Anime could not be deleted");

            var error = new ErrorModel { Message = "Anime not found", StatusCode = 404 };
            return NotFound(error);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "An error occurred while deleting the anime");

            var error = new ErrorModel { Message = "Anime not found", StatusCode = 404 };
            return NotFound(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the anime");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get animes by name
    /// </summary>
    [HttpGet("name/{name}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAnimesByName([FromRoute] string name, [FromQuery] PaginationParams paginationParams)
    {
        try
        {
            _logger.LogInformation("GetAnimesByName called");

            var animes = await _mediator.Send(new GetAnimesByNameQuery(name, paginationParams));
            return Ok(animes);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "An error occurred while getting the animes by name");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 400 };
            return BadRequest(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the animes by name");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get animes by director id
    /// </summary>
    [HttpGet("director/{directorId:int:min(1)}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAnimesByDirector([FromRoute] int directorId, [FromQuery] PaginationParams paginationParams)
    {
        try
        {
            _logger.LogInformation("GetAnimesByDirector called");

            var animes = await _mediator.Send(new GetAnimesByDirectorIdQuery(directorId, paginationParams));
            return Ok(animes);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "An error occurred while getting the animes by director");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 500 };
            return BadRequest(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the animes by director");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get animes by director name
    /// </summary>
    [HttpGet("director/name/{directorName}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAnimesByDirectorName([FromRoute] string directorName, [FromQuery] PaginationParams paginationParams)
    {
        try
        {
            _logger.LogInformation("GetAnimesByDirectorName called");

            var animes = await _mediator.Send(new GetAnimesByDirectorNameQuery(directorName, paginationParams));
            return Ok(animes);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "An error occurred while getting the animes by director name");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 400 };
            return BadRequest(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the animes by director name");

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get animes by summary keyword
    /// </summary>
    [HttpGet("summary/{keyword}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAnimesBySummaryKeyword([FromRoute] string keyword, [FromQuery] PaginationParams paginationParams)
    {
        try
        {
            _logger.LogInformation("GetAnimesBySummaryKeyword called");

            var animes = await _mediator.Send(new GetAnimesBySummaryKeywordQuery(keyword, paginationParams));
            return Ok(animes);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "An error occurred while getting the animes by summary keyword");

            var error = new ErrorModel { Message = ex.Message, StatusCode = 400 };
            return BadRequest(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the animes by summary keyword");

            return StatusCode(500);
        }
    }
}