using FreeHost.Infrastructure.Models.Authorization;

namespace FreeHost.Infrastructure.Models.Hosting;

public class Place
{
    public int Id { get; set; }
    public string Name { get; set; }
    public City City { get; set; }
    public string Address { get; set; }
    public IEnumerable<AvailableDate> AvailableDates { get; set; }
    public int NumberOfBeds { get; set; }
    public IEnumerable<Amenity> Amenities { get; set; }
    public IEnumerable<Photo> Photos { get; set; }
    public int DistanceFromTheCenter { get; set; }
    public int Price { get; set; }
    public User User { get; set; }
}