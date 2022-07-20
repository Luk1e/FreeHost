using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreeHost.Infrastructure.Database;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<AvailableDate> AvailableDates { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<AmenityPlace> AmenityPlace { get; set; }

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

        builder.Entity<AmenityPlace>()
            .HasKey(k => new { k.AmenitiesId, k.PlacesId });
        
        builder.Entity<Place>()
            .HasMany(x => x.Amenities)
            .WithMany(x => x.Places)
            .UsingEntity<AmenityPlace>();

        builder.Entity<AvailableDate>()
            .HasOne<Place>()
            .WithMany(x => x.AvailableDates)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
