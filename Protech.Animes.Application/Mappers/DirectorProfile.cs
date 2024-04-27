using AutoMapper;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Application.Mappers;

public class DirectorProfile : Profile
{
    public DirectorProfile()
    {
        CreateMap<Director, DirectorDto>().ReverseMap();

        CreateMap<UpdateDirectorDto, Director>().ReverseMap();

        CreateMap<CreateDirectorDto, Director>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}