using AutoMapper;
using FreeHost.Infrastructure.Models.DTOs;
using FreeHost.Infrastructure.Models.Hosting;
using FreeHost.Infrastructure.Models.Responses;
using FreeHost.Infrastructure.Models.Utils;

namespace FreeHost.Domain.Mapper.Converters;

public class PaginationBookingResponseConverter :ITypeConverter<PaginationResult<BookedPlace>, BookingResponse>
{
    public BookingResponse Convert(PaginationResult<BookedPlace> source, BookingResponse destination, ResolutionContext context)
    {
        return new BookingResponse {Bookings = new List<BookingDto>(), Page = source.CurrentPage, MaxPage = source.MaxPage};
    }
}