using Microsoft.AspNetCore.Mvc;
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
    private readonly ILogger<AnimeController> _logger;

    public AnimeController(IAnimeService animeService, CreateAnimeUseCase createAnimeUseCase, ILogger<AnimeController> logger)
    {
        _logger = logger;
        _animeService = animeService;
        _createAnimeUseCase = createAnimeUseCase;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    public async Task<IActionResult> GetAnimes()
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

            return StatusCode(500);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AnimeDto), 200)]
    [ProducesResponseType(404)]
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

            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(AnimeDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateAnime(CreateAnimeDto animeDto)
    {
        try
        {
            _logger.LogInformation("CreateAnime called");

            var anime = await _createAnimeUseCase.Execute(animeDto);

            _logger.LogInformation("Anime created");

            return CreatedAtAction(nameof(CreateAnime), new { id = anime.Id }, anime);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the anime");

            return StatusCode(500);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
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

            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the anime");

            return StatusCode(500);
        }
    }

    [HttpGet("director/{directorId}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
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

            return StatusCode(500);
        }
    }
}