namespace FreeHost.Infrastructure.Models.Requests;

public class RegistrationRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Photo { get; set; }
}