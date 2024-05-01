// using Protech.Animes.Application.DTOs;
// using Protech.Animes.Application.Interfaces;

// namespace Protech.Animes.Application.UseCases.AnimeUseCases;

// public class GetAnimesBySummaryKeywordUseCase
// {
//     private readonly IAnimeService _animeService;

//     public GetAnimesBySummaryKeywordUseCase(IAnimeService animeService)
//     {
//         _animeService = animeService;
//     }

//     public async Task<IEnumerable<AnimeDto>> ExecuteAsync(string keyword, int? page, int? pageSize)
//     {
//         if (string.IsNullOrWhiteSpace(keyword))
//             throw new ArgumentException("Keyword cannot be null or empty");

//         if (!page.HasValue || !pageSize.HasValue)
//             return await _animeService.GetAnimesBySummaryKeyword(keyword);

//         if (page.Value <= 0 || pageSize.Value <= 0)
//             throw new ArgumentException("Page and PageSize must be greater than 0");

//         return await _animeService.GetAnimesBySummaryKeywordPaginated(keyword, page.Value, pageSize.Value);
//     }
// }