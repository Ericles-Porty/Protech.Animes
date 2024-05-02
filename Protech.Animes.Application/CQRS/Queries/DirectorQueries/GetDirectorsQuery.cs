using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Queries.DirectorQueries;

public class GetDirectorsQuery(PaginationParams paginationParams) : IRequest<IEnumerable<DirectorDto>>
{
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}