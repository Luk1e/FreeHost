﻿using AutoMapper;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace FreeHost.API.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, RegistrationDto>().ReverseMap();
        CreateMap<AuthorizationDto, RegistrationDto>().ReverseMap();
        CreateMap<AuthenticationResult, IdentityResult>().ReverseMap();
    }
}