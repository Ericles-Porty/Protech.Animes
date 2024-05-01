// using Protech.Animes.Application.DTOs;
// using Protech.Animes.Application.Interfaces;

// namespace Protech.Animes.Application.UseCases.AnimeUseCases;

// public class GetAnimesByNameUseCase
// {
//     private readonly IAnimeService _animeService;

//     public GetAnimesByNameUseCase(IAnimeService animeService)
//     {
//         _animeService = animeService;
//     }

//     public async Task<IEnumerable<AnimeDto>> Execute(string name, int? page, int? pageSize)
//     {
//         if (string.IsNullOrWhiteSpace(name))
//             throw new ArgumentException("Name cannot be null or empty");

//         if (!page.HasValue || !pageSize.HasValue)
//             return await _animeService.GetAnimesByNamePattern(name);

//         if (page.Value <= 0 || pageSize.Value <= 0)
//             throw new ArgumentException("Page and PageSize must be greater than 0");

//         return await _animeService.GetAnimesByNamePatternPaginated(name, page.Value, pageSize.Value);
//     }
// }