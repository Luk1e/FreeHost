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
                Apartment = new BookingPlaceDto
                {
                    BookingId = bookedPlace.Id,
                    Address = bookedPlace.Place.Address,
                    City = bookedPlace.Place.City.Name,
                    Name = bookedPlace.Place.Name,
                    StartDate = bookedPlace.StartDate,
                    EndDate = bookedPlace.EndDate,
                    Photos = bookedPlace.Place.Photos.Select(x => System.Convert.ToBase64String(x.Bytes)),
                    Status = bookedPlace.BookingStatus,
                    Distance = bookedPlace.Place.DistanceFromTheCenter
                },
                User = new BookingUserDto
                {
                    FirstName = bookedPlace.Client.FirstName, 
                    LastName = bookedPlace.Client.LastName, 
                    Photo = System.Convert.ToBase64String(bookedPlace.Client.Photo)
                }
            });
    }
}