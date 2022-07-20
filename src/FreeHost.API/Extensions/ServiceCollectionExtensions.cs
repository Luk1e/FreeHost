using AutoMapper;
using FreeHost.Domain.Mapper;
using FreeHost.Domain.Services;
using FreeHost.Infrastructure.Database;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Repositories;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FreeHost.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<Func<AppDbContext>>(provider => provider.GetService<AppDbContext>);
        services.AddScoped<DbFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
        services.AddScoped<ICityRepo, CityRepo>();
        services.AddScoped<IAmenityRepo, AmenityRepo>();
        services.AddScoped<IPlaceRepo, PlaceRepo>();
        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<IAmenityPlaceRepo, AmenityPlaceRepo>();
        services.AddScoped<IPhotoRepo, PhotoRepo>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IHostingService, HostingService>();

        return services;
    }

    public static IServiceCollection SetIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>();

        return services;
    }

    public static IServiceCollection SetConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetSection("Options").GetSection("AuthOptions"));

        return services;
    }

    public static IServiceCollection SetMapper(this IServiceCollection services)
    {
        

        var mapperConfig = new MapperConfiguration(mc => mc.AddProfile<MappingProfile>());
        var mapper = mapperConfig.CreateMapper();

        services.AddSingleton(mapper);

        return services;
    }

    public static IServiceCollection SetAuthentication(this IServiceCollection services)
    {
        var authConfig = services.BuildServiceProvider().GetRequiredService<IOptions<AuthOptions>>().Value;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authConfig.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(authConfig.Key),
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}