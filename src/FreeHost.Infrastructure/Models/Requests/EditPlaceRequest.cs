namespace FreeHost.Infrastructure.Models.Requests;

public class EditPlaceRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public IEnumerable<DateTime> AvailableDates { get; set; }
    public int NumberOfBeds { get; set; }
    public IEnumerable<string> Amenities { get; set; }
    public IEnumerable<string> Photos { get; set; }
    public int DistanceFromTheCenter { get; set; }
    public int Price { get; set; }
}