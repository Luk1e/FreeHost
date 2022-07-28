using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Options;
using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FreeHost.Domain.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AuthOptions _authConfiguration;
    private readonly IRepository<RefreshToken> _refreshTokenRepo;
    private readonly IMapper _mapper;

    public AuthorizationService(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<AuthOptions> authConfiguration, IRefreshTokenRepo refreshTokenRepo, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authConfiguration = authConfiguration.Value;
        _refreshTokenRepo = refreshTokenRepo;
        _mapper = mapper;
    }

    public async Task<AuthenticationResult> AuthorizeAsync(AuthorizationRequest request)
    {
        var user = _userManager.Users.SingleOrDefault(l => l.Login == request.Login);
        if (user == null)
            throw new NullReferenceException("User was not found.");

        var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!passwordCheck.Succeeded)
            throw new UnauthorizedAccessException("Incorrect login or password.");

        var authResult = await GenerateAuthenticationResultForUserAsync(user);
        authResult.FirstName = user.FirstName;
        authResult.LastName = user.LastName;

        return authResult;
    }

    public async Task<AuthenticationResult> RegisterAsync(RegistrationRequest request)
    {
        if (_userManager.Users.SingleOrDefault(l => l.Login == request.Login) is not null)
        {
            var errors = new List<ErrorResponse> { new() { Code = "DuplicateLogin", Description = $"Login '{request.Login}' is already taken." } };
            return new AuthenticationResult
            {
                Succeeded = false,
                Errors = errors
            };
        }
        if (_userManager.Users.SingleOrDefault(l => l.Email == request.Email) is not null)
        {
            var errors = new List<ErrorResponse> { new() { Code = "DuplicateEmail", Description = $"Email '{request.Email}' is already taken." } };
            return new AuthenticationResult
            {
                Succeeded = false,
                Errors = errors
            };
        }

        var user = _mapper.Map<User>(request);
        user.UserName = user.Email;
        var result = await _userManager.CreateAsync(user, request.Password);
        return result.Succeeded
            ? await AuthorizeAsync(_mapper.Map<AuthorizationRequest>(request))
            : _mapper.Map<AuthenticationResult>(result);
    }

    public async Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _authConfiguration.Issuer,
            ValidateAudience = true,
            ValidAudience = _authConfiguration.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(_authConfiguration.Key),
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.Zero
        };

        var validatedToken = GetPrincipalFromToken(request.AccessToken, tokenValidationParameters);
        if (validatedToken is null)
            return new AuthenticationResult { Errors = new[] { new ErrorResponse { Code = "InvalidToken", Description = "Could not validate access token" } } };

        var expiryDateUnix =
            long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
        var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddSeconds(expiryDateUnix);
        if (expiryDateTimeUtc > DateTime.UtcNow)
            return new AuthenticationResult { Errors = new[] { new ErrorResponse { Code = "NotExpired", Description = "This access token hasn't expired yet" } } };

        var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
        var storedRefreshToken = await _refreshTokenRepo.Get(x => x.Token == request.RefreshToken).SingleOrDefaultAsync();
        if (storedRefreshToken is null)
            return new AuthenticationResult { Errors = new[] { new ErrorResponse { Code = "TokenNotFound", Description = "This refresh token does not exist" } } };

        if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            return new AuthenticationResult { Errors = new[] { new ErrorResponse { Code = "NotExpired", Description = "This refresh token has expired" } } };

        if (storedRefreshToken.Invalidated)
            return new AuthenticationResult { Errors = new[] { new ErrorResponse { Code = "InvalidToken", Description = "This refresh token has been invalidated" } } };

        if (storedRefreshToken.Used)
            return new AuthenticationResult { Errors = new[] { new ErrorResponse { Code = "InvalidToken", Description = "This refresh token has been used" } } };

        if (storedRefreshToken.JwtId != jti)
            return new AuthenticationResult { Errors = new[] { new ErrorResponse { Code = "InvalidToken", Description = "This refresh token does not match this JWT" } } };

        storedRefreshToken.Used = true;
        _refreshTokenRepo.Update(storedRefreshToken);

        var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "userId").Value); // beware of magic claim name

        var authResult = await GenerateAuthenticationResultForUserAsync(user);
        authResult.FirstName = user.FirstName;
        authResult.LastName = user.LastName;

        return authResult;
    }

    private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId", user.Id)
            }, "Token"),
            Audience = _authConfiguration.Audience,
            Issuer = _authConfiguration.Issuer,
            Expires = DateTime.UtcNow.AddMinutes(_authConfiguration.Lifetime),
            SigningCredentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_authConfiguration.Key), SecurityAlgorithms.HmacSha256)
        };

        var accessToken = tokenHandler.CreateToken(tokenDescriptor);

        var refreshToken = new RefreshToken
        {
            Token = RandomString(25) + Guid.NewGuid(),
            JwtId = accessToken.Id,
            UserId = user.Id,
            CreationTime = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(_authConfiguration.RefreshTokenLifetime)
        };

        _refreshTokenRepo.Add(refreshToken);

        return new AuthenticationResult
        {
            Succeeded = true,
            Token = tokenHandler.WriteToken(accessToken),
            RefreshToken = refreshToken.Token
        };
    }

    private static string RandomString(int size)
    {
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        var data = new byte[4 * size];
        using (var crypto = RandomNumberGenerator.Create())
        {
            crypto.GetBytes(data);
        }
        var result = new StringBuilder(size);
        for (var i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * 4);
            var idx = rnd % chars.Length;

            result.Append(chars[idx]);
        }

        return result.ToString();
    }

    private static ClaimsPrincipal GetPrincipalFromToken(string token, TokenValidationParameters tokenValidationParameters)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenValidationParameters.ValidateLifetime = false;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
            tokenValidationParameters.ValidateLifetime = true;
            return !IsJwtWithValidSecurityAlgorithm(validatedToken) ? null : principal;
        }
        catch
        {
            return null;
        }
    }

    private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return validatedToken is JwtSecurityToken jwtSecurityToken &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                   StringComparison.InvariantCultureIgnoreCase);
    }
}