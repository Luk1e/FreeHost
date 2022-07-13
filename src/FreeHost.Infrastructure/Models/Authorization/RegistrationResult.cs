using Microsoft.AspNetCore.Identity;

namespace FreeHost.Infrastructure.Models.Authorization;

public class RegistrationResult
{
    public User User { get; set; }
    public IdentityResult IdentityResult { get; set; }
}