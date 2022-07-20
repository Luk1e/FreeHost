using FreeHost.Infrastructure.Models.Responses;

namespace FreeHost.Infrastructure.Models.Authorization;

public class AuthenticationResult
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public bool Succeeded { get; set; }
    public IEnumerable<ErrorResponse> Errors { get; set; } = Enumerable.Empty<ErrorResponse>();
    public string FirstName { get; set; }
    public string LastName { get; set; }
}