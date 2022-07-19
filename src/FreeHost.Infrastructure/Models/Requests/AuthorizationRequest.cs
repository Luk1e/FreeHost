namespace FreeHost.Infrastructure.Models.Requests;

public class AuthorizationRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
}