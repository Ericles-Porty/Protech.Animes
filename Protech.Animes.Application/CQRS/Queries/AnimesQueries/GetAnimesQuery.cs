using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Queries.AnimesQueries;

public class GetAnimesQuery(PaginationParams paginationParams) : IRequest<IEnumerable<AnimeDto>>
{
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}