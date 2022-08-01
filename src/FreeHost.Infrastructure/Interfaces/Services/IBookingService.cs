using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;

namespace FreeHost.Infrastructure.Interfaces.Services;

public interface IBookingService
{
    BookingResponse GetBookings(string userId, int page);
    BookingResponse GetGuests(string userId, int page);
    void Book(BookingRequest request, string userId);
    void Approve(int bookingId, string userId);
    void Reject(int bookingId, string userId);
}