using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Queries.AnimesQueries;

public class GetAnimesBySummaryKeywordQuery(string keyword, PaginationParams paginationParams) : IRequest<IEnumerable<AnimeDto>>
{
    public string Keyword { get; set; } = keyword;
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}