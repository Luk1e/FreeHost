using AutoMapper;
using FreeHost.Domain.Mapper.Converters;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.DTOs;
using FreeHost.Infrastructure.Models.Hosting;
using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;
using FreeHost.Infrastructure.Models.Utils;
using Microsoft.AspNetCore.Identity;

namespace FreeHost.Domain.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<string, byte[]>().ConvertUsing(x => Convert.FromBase64String(x));
        CreateMap<byte[], string>().ConvertUsing(x => Convert.ToBase64String(x));

        CreateMap<User, RegistrationRequest>().ReverseMap();
        CreateMap<AuthorizationRequest, RegistrationRequest>().ReverseMap();
        CreateMap<IdentityResult, AuthenticationResult>().ConvertUsing<AuthenticationResultIdentityResultConverter>();
        CreateMap<User, UserDto>();
        CreateMap<User, UserProfileResponse>().ConvertUsing<UserToProfileConverter>();

        CreateMap<string, City>().ConvertUsing<StringToCityConverter>();
        CreateMap<City, string>().ConvertUsing<CityToStringConverter>();
        CreateMap<IEnumerable<string>, IEnumerable<Amenity>>().ConvertUsing<StringToAmenityConverter>();
        CreateMap<IEnumerable<Amenity>, IEnumerable<string>>().ConvertUsing<AmenityToStringConverter>();
        CreateMap<IEnumerable<string>, IEnumerable<Photo>>().ConvertUsing<StringToPhotoConverter>();
        CreateMap<IEnumerable<Photo>, IEnumerable<string>>().ConvertUsing<PhotoToStringConverter>();

        CreateMap<AddPlaceRequest, Place>();
        CreateMap<EditPlaceRequest, Place>();
        CreateMap<BookedDate, BookedDatesDto>().ReverseMap();
        CreateMap<Place, PlaceDto>();
        CreateMap<Place, PlaceSearchDto>().ReverseMap();
        CreateMap<IEnumerable<BookedPlace>, IEnumerable<BookingDto>>().ConvertUsing<BookedPlaceBookingResponseConverter>();
        CreateMap<PaginationResult<PlaceSearchDto>, SearchResponse>().ConvertUsing<PaginationSearchResponseConverter>();
        CreateMap<PaginationResult<BookedPlace>, BookingResponse>().ConvertUsing<PaginationBookingResponseConverter>();
    }
}