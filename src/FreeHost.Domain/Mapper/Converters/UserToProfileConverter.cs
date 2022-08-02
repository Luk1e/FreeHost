using AutoMapper;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Responses;

namespace FreeHost.Domain.Mapper.Converters;

public class UserToProfileConverter : ITypeConverter<User, UserProfileResponse>
{
    public UserProfileResponse Convert(User source, UserProfileResponse destination, ResolutionContext context)
    {
        return new UserProfileResponse
        {
            Email = source.Email,
            FirstName = source.FirstName,
            LastName = source.LastName,
            Photo = System.Convert.ToBase64String(source.Photo)
        };
    }
}