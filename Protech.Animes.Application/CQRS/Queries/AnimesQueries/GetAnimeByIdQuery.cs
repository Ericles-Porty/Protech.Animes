using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Queries.AnimesQueries;

public class GetAnimeByIdQuery(int id) : IRequest<AnimeDto>
{
    public int Id { get; set; } = id;
}