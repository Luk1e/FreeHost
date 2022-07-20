using System.IdentityModel.Tokens.Jwt;

namespace FreeHost.API.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserIdFromJwt(this HttpContext context)
    {
        var bearerToken = context.Request.Headers.Authorization.First().Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(bearerToken);
        var userId = jwt.Claims.First(x => x.Type == "userId").Value;

        return userId;
    }
}