using AutoMapper;
using Protech.Animes.Application.CQRS.Commands.AnimeCommands;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Application.Mappers;

public class AnimeProfile : Profile
{
    public AnimeProfile()
    {
        CreateMap<Anime, AnimeDto>()
            .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director.Name));

        CreateMap<CreateAnimeCommand, Anime>()
            .ForMember(dest => dest.Director, opt => opt.Ignore());

        CreateMap<UpdateAnimeCommand, Anime>()
            .ForMember(dest => dest.Director, opt => opt.Ignore());
            
    }
}
