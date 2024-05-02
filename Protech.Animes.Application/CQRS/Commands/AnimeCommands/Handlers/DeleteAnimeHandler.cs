using AutoMapper;
using MediatR;
using Protech.Animes.Domain.Exceptions;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Commands.AnimeCommands.Handlers;

public class DeleteAnimeHandler : IRequestHandler<DeleteAnimeCommand, bool>
{

    private readonly IAnimeService _animeService;

    public DeleteAnimeHandler(IAnimeService animeService)
    {
        _animeService = animeService;
    }

    public async Task<bool> Handle(DeleteAnimeCommand request, CancellationToken cancellationToken)
    {
        var anime = await _animeService.GetAnime(request.Id);
        if (anime is null)
            throw new NotFoundException("Anime not found");

        return await _animeService.DeleteAnime(request.Id);
    }
}