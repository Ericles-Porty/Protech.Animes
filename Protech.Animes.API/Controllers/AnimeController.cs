using Microsoft.AspNetCore.Mvc;
using Protech.Animes.API.Models;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Application.UseCases.Anime;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.API.Controllers;

/// <summary>
/// Controller for anime operations
/// </summary>
/// <response code="500">An error occurred while processing the request</response>
/// <response code="404">The requested resource was not found</response>
/// <response code="400">The request is invalid</response>
/// <response code="201">The resource was created</response>
/// <response code="204">The resource was deleted</response>
/// <response code="200">The resource was found</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
[ApiController]
[Route("api/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly ILogger<AnimeController> _logger;
    private readonly CreateAnimeUseCase _createAnimeUseCase;
    private readonly UpdateAnimeUseCase _updateAnimeUseCase;
    private readonly GetAnimesUseCase _getAnimesUseCase;
    private readonly GetAnimeUseCase _getAnimeUsecase;
    private readonly DeleteAnimeUseCase _deleteAnimeUseCase;
    private readonly GetAnimesByNameUseCase _getAnimesByNameUseCase;
    private readonly GetAnimesByDirectorUseCase _getAnimesByDirectorUseCase;
    private readonly GetAnimesByDirectorNameUseCase _getAnimesByDirectorNameUseCase;
    private readonly GetAnimesBySummaryKeywordUseCase _getAnimesBySummaryKeywordUseCase;

    public AnimeController(
        ILogger<AnimeController> logger,
        CreateAnimeUseCase createAnimeUseCase,
        UpdateAnimeUseCase updateAnimeUseCase,
        GetAnimesUseCase getAnimesUseCase,
        GetAnimeUseCase getAnimeUsecase,
        DeleteAnimeUseCase deleteAnimeUseCase,
        GetAnimesByNameUseCase getAnimesByNameUseCase,
        GetAnimesByDirectorUseCase getAnimesByDirectorUseCase,
        GetAnimesByDirectorNameUseCase getAnimesByDirectorNameUseCase,
        GetAnimesBySummaryKeywordUseCase getAnimesBySummaryKeywordUseCase
        )
    {
        _logger = logger;
        _createAnimeUseCase = createAnimeUseCase;
        _updateAnimeUseCase = updateAnimeUseCase;
        _getAnimesUseCase = getAnimesUseCase;
        _getAnimeUsecase = getAnimeUsecase;
        _deleteAnimeUseCase = deleteAnimeUseCase;
        _getAnimesByNameUseCase = getAnimesByNameUseCase;
        _getAnimesByDirectorUseCase = getAnimesByDirectorUseCase;
        _getAnimesByDirectorNameUseCase = getAnimesByDirectorNameUseCase;
        _getAnimesBySummaryKeywordUseCase = getAnimesBySummaryKeywordUseCase;
    }

    /// <summary>
    /// Get all animes
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> GetAnimes([FromQuery] int? page, [FromQuery] int? pageSize)
    {
        try
        {
            _logger.LogInformation("GetAnimes called");

            var animes = await _getAnimesUseCase.Execute(page, pageSize);

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

            var error = new ErrorModel { Message = "An error occurred while getting the animes", StatusCode = 500 };

            return StatusCode(500, error);
        }
    }

    /// <summary>
    /// Get an anime by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Returns the anime</response>
    /// <response code="404">The requested resource was not found</response>
    /// <response code="500">An error occurred while processing the request</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AnimeDto), 200)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> GetAnime(int id)
    {
        try
        {
            _logger.LogInformation($"GetAnime called with id {id}");

            var anime = await _getAnimeUsecase.Execute(id);

            _logger.LogInformation($"Anime with id {id} found");

            return Ok(anime);
        }
        catch (NotFoundException)
        {
            _logger.LogWarning($"Anime with id {id} not found");

            var error = new ErrorModel { Message = "Anime not found", StatusCode = 404 };

            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the anime");

            var error = new ErrorModel { Message = "An error occurred while getting the anime", StatusCode = 500 };

            return StatusCode(500, error);
        }
    }

    /// <summary>
    /// Create an anime
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AnimeDto), 201)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> CreateAnime(CreateAnimeDto animeDto)
    {
        try
        {
            _logger.LogInformation("CreateAnime called");

            var createdAnime = await _createAnimeUseCase.Execute(animeDto);

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

            var error = new ErrorModel { Message = "An error occurred while creating the anime", StatusCode = 500 };

            return StatusCode(500, error);
        }
    }

    /// <summary>
    /// Update an anime
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdateAnimeDto), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> UpdateAnime(int id, UpdateAnimeDto updateAnimeDto)
    {
        try
        {
            _logger.LogInformation($"UpdateAnime called with id {id}");

            var updatedAnime = await _updateAnimeUseCase.Execute(id, updateAnimeDto);

            _logger.LogInformation($"Anime with id {id} updated");

            return Ok(updatedAnime);
        }
        catch (NotFoundException)
        {
            _logger.LogWarning($"Anime with id {id} not found");

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

            var error = new ErrorModel { Message = "An error occurred while updating the anime", StatusCode = 500 };

            return StatusCode(500, error);
        }
    }

    /// <summary>
    /// Delete an anime by id
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> DeleteAnime(int id)
    {
        try
        {
            _logger.LogInformation($"DeleteAnime called with id {id}");

            var deleted = await _deleteAnimeUseCase.Execute(id);


            if (deleted is true)
            {
                _logger.LogInformation($"Anime with id {id} deleted");

                return NoContent();
            }

            _logger.LogWarning($"Anime with id {id} could not be deleted");

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

            var error = new ErrorModel { Message = "An error occurred while deleting the anime", StatusCode = 500 };

            return StatusCode(500, error);
        }
    }

    /// <summary>
    /// Get animes by name
    /// </summary>
    [HttpGet("name/{name}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> GetAnimesByName(string name, [FromQuery] int? page, [FromQuery] int? pageSize)
    {
        try
        {
            _logger.LogInformation($"GetAnimesByName called with name {name}");

            var animes = await _getAnimesByNameUseCase.Execute(name, page, pageSize);

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

            var error = new ErrorModel { Message = "An error occurred while getting the animes by name", StatusCode = 500 };

            return StatusCode(500, error);
        }
    }

    /// <summary>
    /// Get animes by director id
    /// </summary>
    [HttpGet("director/{directorId}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> GetAnimesByDirector(int directorId, [FromQuery] int? page, [FromQuery] int? pageSize)
    {
        try
        {
            _logger.LogInformation($"GetAnimesByDirector called with directorId {directorId}");

            var animes = await _getAnimesByDirectorUseCase.Execute(directorId, page, pageSize);

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

            var error = new ErrorModel { Message = "An error occurred while getting the animes by director", StatusCode = 500 };

            return StatusCode(500, error);
        }
    }

    /// <summary>
    /// Get animes by director name
    /// </summary>
    [HttpGet("director/name/{directorName}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> GetAnimesByDirectorName(string directorName, [FromQuery] int? page, [FromQuery] int? pageSize)
    {
        try
        {
            _logger.LogInformation($"GetAnimesByDirectorName called with directorName {directorName}");

            var animes = await _getAnimesByDirectorNameUseCase.Execute(directorName, page, pageSize);

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

            var error = new ErrorModel { Message = "An error occurred while getting the animes by director name", StatusCode = 500 };

            return StatusCode(500, error);
        }
    }

    /// <summary>
    /// Get animes by summary keyword
    /// </summary>
    [HttpGet("summary/{keyword}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> GetAnimesBySummaryKeyword(string keyword, [FromQuery] int? page, [FromQuery] int? pageSize)
    {
        try
        {
            _logger.LogInformation($"GetAnimesBySummaryKeyword called with keyword {keyword}");

            var animes = await _getAnimesBySummaryKeywordUseCase.ExecuteAsync(keyword, page, pageSize);

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

            var error = new ErrorModel { Message = "An error occurred while getting the animes by summary keyword", StatusCode = 500 };

            return StatusCode(500, error);
        }
    }
}