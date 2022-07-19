using AutoMapper;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace FreeHost.API.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, RegistrationRequest>().ReverseMap();
        CreateMap<AuthorizationRequest, RegistrationRequest>().ReverseMap();
        CreateMap<AuthenticationResult, IdentityResult>().ReverseMap();
    }
}