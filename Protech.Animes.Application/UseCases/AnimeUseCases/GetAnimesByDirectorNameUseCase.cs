// using Protech.Animes.Application.DTOs;
// using Protech.Animes.Application.Interfaces;

// namespace Protech.Animes.Application.UseCases.AnimeUseCases;

// public class GetAnimesByDirectorNameUseCase
// {
//     private readonly IAnimeService _animeService;

//     public GetAnimesByDirectorNameUseCase(IAnimeService animeService)
//     {
//         _animeService = animeService;
//     }

//     public async Task<IEnumerable<AnimeDto>> Execute(string directorName, int? page, int? pageSize)
//     {
//         if (string.IsNullOrWhiteSpace(directorName))
//             throw new ArgumentException("Director name cannot be null or empty");

//         if (!page.HasValue || !pageSize.HasValue)
//             return await _animeService.GetAnimesByDirectorName(directorName);

//         if (page <= 0 || pageSize <= 0)
//             throw new ArgumentException("Page and page size must be greater than 0");

//         return await _animeService.GetAnimesByDirectorNamePaginated(directorName, page.Value, pageSize.Value);
//     }
// }