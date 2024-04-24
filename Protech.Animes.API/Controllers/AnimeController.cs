using Microsoft.AspNetCore.Mvc;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Application.UseCases;

namespace Protech.Animes.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly IAnimeService _animeService;
    private readonly CreateAnimeUseCase _createAnimeUseCase;

    public AnimeController(IAnimeService animeService, CreateAnimeUseCase createAnimeUseCase)
    {
        _animeService = animeService;
        _createAnimeUseCase = createAnimeUseCase;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    public async Task<IActionResult> GetAnimes()
    {
        var animes = await _animeService.GetAnimes();
        return Ok(animes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AnimeDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAnime(int id)
    {
        var anime = await _animeService.GetAnime(id);
        return Ok(anime);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AnimeDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateAnime(CreateAnimeDto animeDto)
    {
        var anime = await _createAnimeUseCase.Execute(animeDto);

        return CreatedAtAction(nameof(CreateAnime), new { id = anime.Id }, anime);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteAnime(int id)
    {
        var deleted = await _animeService.DeleteAnime(id);
        if (deleted) return NoContent();
        return NotFound();
    }

    [HttpGet("director/{directorId}")]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    public async Task<IActionResult> GetAnimesByDirector(int directorId)
    {
        var animes = await _animeService.GetAnimesByDirector(directorId);
        return Ok(animes);
    }
}