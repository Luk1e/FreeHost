namespace FreeHost.Infrastructure.Models.DTOs;

public class BookingDto
{
    public int ApartmentId { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public UserDto User { get; set; }
}