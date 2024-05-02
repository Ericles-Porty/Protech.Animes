using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Queries.AnimesQueries;

public class GetAnimesByDirectorNameQuery(string directorName, PaginationParams paginationParams) : IRequest<IEnumerable<AnimeDto>>
{
    public string DirectorName { get; set; } = directorName;
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}
