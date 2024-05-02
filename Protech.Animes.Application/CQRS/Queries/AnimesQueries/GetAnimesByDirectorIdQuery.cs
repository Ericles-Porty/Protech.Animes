using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Queries.AnimesQueries;

public class GetAnimesByDirectorIdQuery(int directorId, PaginationParams paginationParams) : IRequest<IEnumerable<AnimeDto>>
{
    public int DirectorId { get; set; } = directorId;
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}
