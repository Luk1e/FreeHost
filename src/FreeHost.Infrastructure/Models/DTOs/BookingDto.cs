namespace FreeHost.Infrastructure.Models.DTOs;

public class BookingDto
{
    public BookingPlaceDto Apartment { get; set; }
    public BookingUserDto User { get; set; }
}