using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Repositories;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.DTOs;
using FreeHost.Infrastructure.Models.Options;
using Microsoft.AspNetCore.Identity;
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

    public async Task<AuthenticationResult> AuthorizeAsync(AuthorizationDto dto)
    {
        var user = _userManager.Users.SingleOrDefault(l => l.Login == dto.Login);
        if (user == null)
            throw new NullReferenceException("User was not found");

        var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!passwordCheck.Succeeded)
            throw new UnauthorizedAccessException();
        

        return await GenerateAuthenticationResultForUserAsync(user);
    }

    public async Task<RegistrationResult> RegisterAsync(RegistrationDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var result = await _userManager.CreateAsync(user, userDto.Password);
        return new RegistrationResult{User = user, IdentityResult = result};
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
            ExpiryDate = DateTime.UtcNow.AddMonths(_authConfiguration.RefreshTokenLifetime)
        };

        _refreshTokenRepo.Add(refreshToken);

        return new AuthenticationResult
        {
            Success = true,
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


}