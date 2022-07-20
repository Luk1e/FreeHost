using Microsoft.AspNetCore.Identity;

namespace FreeHost.Infrastructure.Models.Authorization;

public class AuthenticationResult
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public bool Succeeded { get; set; }
    public IEnumerable<IdentityError> Errors { get; set; } = Enumerable.Empty<IdentityError>();
    public string FirstName { get; set; }
    public string LastName { get; set; }
}