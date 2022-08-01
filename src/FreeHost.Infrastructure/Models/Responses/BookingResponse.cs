using FreeHost.Infrastructure.Models.DTOs;

namespace FreeHost.Infrastructure.Models.Responses;

public class BookingResponse
{
    public IEnumerable<BookingDto> Bookings { get; set; }
    public int Page { get; set; }
    public int MaxPage { get; set; }
}