using Microsoft.AspNetCore.Identity;

namespace FreeHost.Infrastructure.Models;

public class User : IdentityUser
{
    public string Login { get; set; }
    public string UserName { get; set; }
    public byte[] Photo { get; set; }
}