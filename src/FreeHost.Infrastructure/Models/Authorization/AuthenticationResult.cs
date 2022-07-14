namespace FreeHost.Infrastructure.Models.Authorization;

public class AuthenticationResult
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
}