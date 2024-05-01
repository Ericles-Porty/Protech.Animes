// using Protech.Animes.Application.DTOs;
// using Protech.Animes.Application.Interfaces;

// namespace Protech.Animes.Application.UseCases.AnimeUseCases;

// public class GetAnimesByDirectorUseCase
// {
//     private readonly IAnimeService _animeService;

//     public GetAnimesByDirectorUseCase(IAnimeService animeService)
//     {
//         _animeService = animeService;
//     }

//     public async Task<IEnumerable<AnimeDto>> Execute(int directorId, int? page, int? pageSize)
//     {
//         if (!page.HasValue || !pageSize.HasValue)
//             return await _animeService.GetAnimesByDirector(directorId);

//         if (page <= 0 || pageSize <= 0)
//             throw new ArgumentException("Invalid page or pageSize");

//         return await _animeService.GetAnimesByDirectorPaginated(directorId, page.Value, pageSize.Value);
//     }
// }