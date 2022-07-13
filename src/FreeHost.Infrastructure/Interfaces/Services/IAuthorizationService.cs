using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.DTOs;

namespace FreeHost.Infrastructure.Interfaces.Services;

public interface IAuthorizationService
{
    Task<AuthenticationResult> AuthorizeAsync(AuthorizationDto dto);
    Task<RegistrationResult> RegisterAsync(RegistrationDto user);
}