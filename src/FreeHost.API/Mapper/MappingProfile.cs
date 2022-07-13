using AutoMapper;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.DTOs;

namespace FreeHost.API.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, RegistrationDto>().ReverseMap();
    }
}