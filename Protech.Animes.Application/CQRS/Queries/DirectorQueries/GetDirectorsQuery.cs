using System.ComponentModel.DataAnnotations;
using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Queries.DirectorQueries;

public class GetDirectorsQuery : IRequest<IEnumerable<DirectorDto>>
{
    [Range(1, int.MaxValue)]
    public int? Page { get; set; }

    [Range(1, int.MaxValue)]
    public int? PageSize { get; set; }
}