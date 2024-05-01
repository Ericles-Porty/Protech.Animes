using AutoMapper;
using Protech.Animes.Application.CQRS.Commands.DirectorCommands;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Application.Mappers;

public class DirectorProfile : Profile
{
    public DirectorProfile()
    {
        CreateMap<Director, DirectorDto>().ReverseMap();
        CreateMap<CreateDirectorCommand, Director>().ReverseMap();
        CreateMap<UpdateDirectorDto, Director>().ReverseMap();
    }
}