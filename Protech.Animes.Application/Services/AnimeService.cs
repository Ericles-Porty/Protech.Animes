using AutoMapper;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Infrastructure.Data.Repositories.Interfaces;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.Application.Services;

public class AnimeService : IAnimeService
{

    private readonly IAnimeRepository _animeRepository;
    private readonly IMapper _mapper;

    public AnimeService(IAnimeRepository animeRepository, IMapper mapper)
    {
        _animeRepository = animeRepository;
        _mapper = mapper;
    }

    public async Task<AnimeDto> CreateAnime(AnimeDto animeDto)
    {
        var anime = _mapper.Map<Anime>(animeDto);

        var createdAnime = await _animeRepository.CreateAsync(anime);

        var animeDtoCreated = _mapper.Map<AnimeDto>(createdAnime);

        return animeDtoCreated;
    }

    public async Task<AnimeDto> CreateAnimeWithDirector(AnimeDto animeDto)
    {
        var director = new Director { Name = animeDto.DirectorName };

        var anime = _mapper.Map<Anime>(animeDto);

        var createdAnime = await _animeRepository.CreateAnimeWithDirectorAsync(anime, director);

        if (createdAnime is null) throw new Exception("An error occurred while creating the anime");

        var animeDtoCreated = _mapper.Map<AnimeDto>(createdAnime);

        return animeDtoCreated;
    }

    public async Task<bool> DeleteAnime(int id)
    {
        return await _animeRepository.DeleteAsync(id);
    }

    public async Task<AnimeDto> GetAnime(int id)
    {
        var anime = await _animeRepository.GetByIdIncludingDirectorAsync(id);

        if (anime is null) throw new NotFoundException("Anime not found");

        var animeDto = _mapper.Map<AnimeDto>(anime);

        return animeDto;
    }

    public async Task<AnimeDto?> GetAnimeByName(string name)
    {
        var anime = await _animeRepository.GetByNameAsync(name);

        if (anime is null) return null;

        var animeDto = _mapper.Map<AnimeDto>(anime);

        return animeDto;
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimesByNamePattern(string name)
    {
        var animes = await _animeRepository.GetByNamePatternAsync(name);

        var animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);

        return animesDto;
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimesByNamePatternPaginated(string name, int page, int pageSize)
    {
        var animes = await _animeRepository.GetByNamePatternPaginatedAsync(name, page, pageSize);

        var animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);

        return animesDto;
    }


    public async Task<IEnumerable<AnimeDto>> GetAnimes()
    {
        var animes = await _animeRepository.GetAllAsync();

        var animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);

        return animesDto;
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimesByDirector(int directorId)
    {
        var animes = await _animeRepository.GetByDirectorIdAsync(directorId);

        var animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);

        return animesDto;
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimesByDirectorPaginated(int directorId, int page, int pageSize)
    {
        var animes = await _animeRepository.GetByDirectorIdPaginatedAsync(directorId, page, pageSize);

        var animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);

        return animesDto;
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimesBySummaryKeyword(string keyword)
    {
        var animes = await _animeRepository.GetByKeywordSummaryAsync(keyword);

        var animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);

        return animesDto;
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimesBySummaryKeywordPaginated(string keyword, int page, int pageSize)
    {
        var animes = await _animeRepository.GetByKeywordSummaryPaginatedAsync(keyword, page, pageSize);

        var animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);

        return animesDto;
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimesPaginated(int page, int pageSize)
    {
        var animes = await _animeRepository.GetAllPaginatedAsync(page, pageSize);

        var animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);

        return animesDto;
    }

    public async Task<AnimeDto> UpdateAnime(int id, AnimeDto animeDto)
    {
        var anime = _mapper.Map<Anime>(animeDto);

        var updatedAnime = await _animeRepository.UpdateAsync(id, anime);

        if (updatedAnime is null) throw new NotFoundException("Anime not found");

        var animeDtoUpdated = _mapper.Map<AnimeDto>(updatedAnime);

        return animeDtoUpdated;
    }

    public async Task<AnimeDto> UpdateAnimeWithNewDirector(AnimeDto animeDto)
    {
        var director = new Director { Name = animeDto.DirectorName };

        var anime = _mapper.Map<Anime>(animeDto);

        var updatedAnime = await _animeRepository.UpdateAnimeWithNewDirectorAsync(anime, director);

        if (updatedAnime is null) throw new NotFoundException("Anime not found");

        var animeDtoUpdated = _mapper.Map<AnimeDto>(updatedAnime);

        return animeDtoUpdated;
    }
}