using System.Reflection;
using AutoMapper;
using FreeHost.Domain.Mapper;
using FreeHost.Domain.Services;
using FreeHost.Infrastructure.Database;
using FreeHost.Infrastructure.Database.Repos;
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
using Microsoft.OpenApi.Models;

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
        services.AddScoped<IBookedPlaceRepo, BookedPlaceRepo>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IHostingService, HostingService>();
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<IBookingService, BookingService>();

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

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Name = "Authorization",
                Description = @"JWT Authorization header using the Bearer scheme.
                      Enter your token in the text input below.
                      Example: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.----'",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    jwtSecurityScheme, Array.Empty<string>()
                }
            });

            options.CustomSchemaIds(o => o.FullName);

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        /*services.AddSwaggerGenWithConventionalRoutes(options =>
        {
            options.IgnoreTemplateFunc = (template) => template.StartsWith("api/");
            options.SkipDefaults = true;
        });

        services.AddSwaggerGenNewtonsoftSupport();*/

        return services;
    }
}