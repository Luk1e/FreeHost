using AutoMapper;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Identity;

namespace FreeHost.Domain.Mapper.Converters;

public class AuthenticationResultIdentityResultConverter : ITypeConverter<IdentityResult, AuthenticationResult>
{
    public AuthenticationResult Convert(IdentityResult source, AuthenticationResult destination, ResolutionContext context)
    {
        return new AuthenticationResult
        {
            Errors = source.Errors.Select(x => new ErrorResponse {Code = x.Code, Description = x.Description}),
            Succeeded = source.Succeeded
        };
    }
}