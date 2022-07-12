using FreeHost.Infrastructure;
using FreeHost.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreeHost.Web.Extensions;

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

        return services;
    }
}