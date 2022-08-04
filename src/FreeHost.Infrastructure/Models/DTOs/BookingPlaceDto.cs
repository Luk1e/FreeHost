using FreeHost.Infrastructure.Models.Enums;

namespace FreeHost.Infrastructure.Models.DTOs;

public class BookingPlaceDto
{
    public int BookingId { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public IEnumerable<string> Photos { get; set; }
    public BookingStatusEnum Status { get; set; }
    public int Distance { get; set; }
}