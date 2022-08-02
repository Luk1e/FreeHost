using FreeHost.Infrastructure.Models.DTOs;

namespace FreeHost.Infrastructure.Models.Responses;

public class BookingResponse
{
    public IEnumerable<BookingDto> Data { get; set; }
    public int Page { get; set; }
    public int MaxPage { get; set; }
}