using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Requests;

namespace FreeHost.Infrastructure.Interfaces.Services;

public interface IAuthorizationService
{
    Task<AuthenticationResult> AuthorizeAsync(AuthorizationRequest request);
    Task<AuthenticationResult> RegisterAsync(RegistrationRequest request);
    Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request);
}