using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Queries.DirectorQueries;

public class GetDirectorByIdQuery(int id) : IRequest<DirectorDto>
{
    public int Id { get; set; } = id;
}