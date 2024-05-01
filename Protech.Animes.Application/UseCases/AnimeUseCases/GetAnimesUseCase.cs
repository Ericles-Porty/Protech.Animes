// using Protech.Animes.Application.DTOs;
// using Protech.Animes.Application.Interfaces;

// namespace Protech.Animes.Application.UseCases.AnimeUseCases;

// public class GetAnimesUseCase
// {
//     private readonly IAnimeService _animeService;

//     public GetAnimesUseCase(IAnimeService animeService)
//     {
//         _animeService = animeService;
//     }

//     public async Task<IEnumerable<AnimeDto>> Execute(int? page, int? pageSize)
//     {
//         if (page.HasValue && pageSize.HasValue)
//         {
//             if (page.Value <= 0 || pageSize.Value <= 0)
//                 throw new ArgumentException("Page and PageSize must be greater than 0");

//             return await _animeService.GetAnimesPaginated(page.Value, pageSize.Value);
//         }

//         return await _animeService.GetAnimes();
//     }
// }