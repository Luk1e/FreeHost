using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FreeHost.Infrastructure.Models.Options;

public class AuthOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int Lifetime { get; set; }
    public string Key { get; set; }
    public int RefreshTokenLifetime { get; set; }

    public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }
}