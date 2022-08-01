using AutoMapper;
using FreeHost.Infrastructure.Models.DTOs;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Domain.Mapper.Converters;

public class BookedPlaceBookingResponseConverter : ITypeConverter<IEnumerable<BookedPlace>, IEnumerable<BookingDto>>
{
    public IEnumerable<BookingDto> Convert(IEnumerable<BookedPlace> source, IEnumerable<BookingDto> destination, ResolutionContext context)
    {
        return source.Select(bookedPlace => new BookingDto
            {
                ApartmentId = bookedPlace.Id,
                Address = bookedPlace.Place.Address,
                City = bookedPlace.Place.City.Name,
                Name = bookedPlace.Place.Name,
                User = new UserDto {FirstName = bookedPlace.Client.FirstName, LastName = bookedPlace.Client.LastName}
            });
    }
}