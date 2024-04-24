using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;

namespace Protech.Animes.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DirectorController : ControllerBase
{

    private readonly IDirectorService _directorService;

    public DirectorController(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DirectorDto>), 200)]
    public async Task<IActionResult> GetDirectors()
    {
        var directors = await _directorService.GetDirectors();
        return Ok(directors);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DirectorDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetDirector(int id)
    {
        var director = await _directorService.GetDirector(id);
        return Ok(director);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DirectorDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateDirector(DirectorDto directorDto)
    {
        var director = await _directorService.CreateDirector(directorDto);

        return CreatedAtAction(nameof(CreateDirector), new { id = director.Id }, director);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteDirector(int id)
    {
        var deleted = await _directorService.DeleteDirector(id);

        if (deleted) return NoContent();

        return NotFound();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DirectorDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateDirector(int id, DirectorDto directorDto)
    {
        var director = await _directorService.UpdateDirector(id, directorDto);
        
        return Ok(director);
    }
}