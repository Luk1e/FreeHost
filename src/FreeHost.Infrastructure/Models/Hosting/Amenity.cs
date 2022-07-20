namespace FreeHost.Infrastructure.Models.Hosting;

public class Amenity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Place> Places { get; set; }
}