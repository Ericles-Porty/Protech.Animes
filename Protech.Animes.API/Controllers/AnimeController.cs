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
        var animes = await _animeService.GetAnimes();

        _logger.LogInformation("GetAnimes called");

        return Ok(animes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AnimeDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAnime(int id)
    {
        try
        {
            var anime = await _animeService.GetAnime(id);

            _logger.LogInformation($"GetAnime called with id {id}");

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
        var anime = await _createAnimeUseCase.Execute(animeDto);

        _logger.LogInformation("CreateAnime called");

        return CreatedAtAction(nameof(CreateAnime), new { id = anime.Id }, anime);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteAnime(int id)
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

    [HttpGet("director/{directorId}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    public async Task<IActionResult> GetAnimesByDirector(int directorId)
    {
        var animes = await _animeService.GetAnimesByDirector(directorId);

        _logger.LogInformation($"GetAnimesByDirector called with directorId {directorId}");

        return Ok(animes);
    }
}