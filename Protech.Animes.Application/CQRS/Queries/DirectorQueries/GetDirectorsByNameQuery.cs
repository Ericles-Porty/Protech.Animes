using System.ComponentModel.DataAnnotations;
using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Queries.DirectorQueries;

public class GetDirectorsByNameQuery(string name, PaginationParams paginationParams) : IRequest<IEnumerable<DirectorDto>>
{
    public string Name { get; set; } = name;
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}