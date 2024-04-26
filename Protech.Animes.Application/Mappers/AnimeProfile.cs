using AutoMapper;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Application.Mappers;

public class AnimeProfile : Profile
{
    public AnimeProfile()
    {
        // dest = destination, opt = options, src = source
        CreateMap<Anime, AnimeDto>()
            .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director.Name));

        CreateMap<AnimeDto, Anime>()
            .ForMember(dest => dest.Director, opt => opt.Ignore());

        CreateMap<CreateAnimeDto, Anime>()
            .ForMember(dest => dest.Director, opt => opt.Ignore());

        CreateMap<Anime, CreateAnimeDto>()
            .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director.Name));

        CreateMap<CreateAnimeDto, AnimeDto>().ReverseMap();

        CreateMap<UpdateAnimeDto, AnimeDto>().ReverseMap();
    }
}
