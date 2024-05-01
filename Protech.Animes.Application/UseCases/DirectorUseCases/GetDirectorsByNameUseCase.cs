// using Protech.Animes.Application.DTOs;
// using Protech.Animes.Application.Interfaces;

// namespace Protech.Animes.Application.UseCases.DirectorUseCases;

// public class GetDirectorsByNameUseCase
// {
//     private readonly IDirectorService _directorService;

//     public GetDirectorsByNameUseCase(IDirectorService directorService)
//     {
//         _directorService = directorService;
//     }

//     public async Task<IEnumerable<DirectorDto>> Execute(string name, int? page, int? pageSize)
//     {
//         if (!page.HasValue || !pageSize.HasValue)
//             return await _directorService.GetDirectorsByNamePattern(name);

//         if (page.Value < 1 || pageSize.Value < 1)
//             throw new ArgumentException("Page and pageSize must be greater than 0");

//         return await _directorService.GetDirectorsByNamePatternPaginated(name, page.Value, pageSize.Value);
//     }
// }