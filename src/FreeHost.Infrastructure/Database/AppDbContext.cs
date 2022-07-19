using FreeHost.Infrastructure.Models.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreeHost.Infrastructure.Database;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>()
            .HasIndex(e => e.Email).IsUnique();
        builder.Entity<User>()
            .HasIndex(l => l.Login).IsUnique();
        builder.Entity<User>()
            .Property(e => e.Email).IsRequired();
    }
}
