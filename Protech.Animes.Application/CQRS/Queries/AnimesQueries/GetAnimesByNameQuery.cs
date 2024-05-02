
using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Queries.AnimesQueries;

public class GetAnimesByNameQuery(string name, PaginationParams paginationParams) : IRequest<IEnumerable<AnimeDto>>
{
    public string Name { get; set; } = name;
    public PaginationParams PaginationParams { get; set; } = paginationParams;

}