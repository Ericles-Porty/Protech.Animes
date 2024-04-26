using Microsoft.AspNetCore.Mvc;
using Protech.Animes.API.Models;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Application.UseCases;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly IAnimeService _animeService;
    private readonly CreateAnimeUseCase _createAnimeUseCase;
    private readonly UpdateAnimeUseCase _updateAnimeUseCase;
    private readonly ILogger<AnimeController> _logger;

    public AnimeController(IAnimeService animeService, CreateAnimeUseCase createAnimeUseCase, ILogger<AnimeController> logger, UpdateAnimeUseCase updateAnimeUseCase)
    {
        _logger = logger;
        _animeService = animeService;
        _createAnimeUseCase = createAnimeUseCase;
        _updateAnimeUseCase = updateAnimeUseCase;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> GetAnimes([FromQuery] int? page, [FromQuery] int? pageSize)
    {
        try
        {
            _logger.LogInformation("GetAnimes called");

            var animes = await _animeService.GetAnimes();

            return Ok(animes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the animes");

            var error = new ErrorModel { Message = "An error occurred while getting the animes", StatusCode = 500 };

            return StatusCode(500, error);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AnimeDto), 200)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> GetAnime(int id)
    {
        try
        {
            _logger.LogInformation($"GetAnime called with id {id}");

            var anime = await _animeService.GetAnime(id);

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

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> DeleteAnime(int id)
    {
        try
        {
            var deleted = await _animeService.DeleteAnime(id);

            _logger.LogInformation($"DeleteAnime called with id {id}");

            if (deleted)
            {
                _logger.LogInformation($"Anime with id {id} deleted");

                return NoContent();
            }

            _logger.LogWarning($"Anime with id {id} could not be deleted");

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

    [HttpGet("director/{directorId}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> GetAnimesByDirector(int directorId)
    {
        try
        {
            _logger.LogInformation($"GetAnimesByDirector called with directorId {directorId}");

            var animes = await _animeService.GetAnimesByDirector(directorId);

            return Ok(animes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the animes by director");

            var error = new ErrorModel { Message = "An error occurred while getting the animes by director", StatusCode = 500 };

            return StatusCode(500, error);
        }
    }
}