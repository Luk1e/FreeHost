using FreeHost.Infrastructure.Models.DTOs;

namespace FreeHost.Infrastructure.Models.Responses;

public class BookingResponse
{
    public int ApartmentId { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public UserDto User { get; set; }
}