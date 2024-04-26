using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.Interfaces;

public interface IAnimeService
{
    Task<IEnumerable<AnimeDto>> GetAnimes();
    Task<IEnumerable<AnimeDto>> GetAnimesPaginated(int page, int pageSize);

    Task<AnimeDto> GetAnime(int id);
    Task<AnimeDto?> GetAnimeByName(string name);

    Task<AnimeDto> CreateAnime(AnimeDto animeDto);
    Task<AnimeDto> CreateAnimeWithDirector(AnimeDto animeDto);

    Task<AnimeDto> UpdateAnime(int id, AnimeDto animeDto);
    Task<AnimeDto> UpdateAnimeWithNewDirector(AnimeDto animeDto);

    Task<bool> DeleteAnime(int id);

    Task<IEnumerable<AnimeDto>> GetAnimesByNamePattern(string name);
    Task<IEnumerable<AnimeDto>> GetAnimesByNamePatternPaginated(string name, int page, int pageSize);

    Task<IEnumerable<AnimeDto>> GetAnimesByDirector(int directorId);
    Task<IEnumerable<AnimeDto>> GetAnimesByDirectorPaginated(int directorId, int page, int pageSize);

    Task<IEnumerable<AnimeDto>> GetAnimesBySummaryKeyword(string keyword);
    Task<IEnumerable<AnimeDto>> GetAnimesBySummaryKeywordPaginated(string keyword, int page, int pageSize);
}