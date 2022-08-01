using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;

namespace FreeHost.Infrastructure.Interfaces.Services;

public interface IBookingService
{
    /*IEnumerable<BookingResponse> GetBookings(string userId);
    IEnumerable<BookingResponse> GetGuests(string userId);*/
    void Book(BookingRequest request, string userId);
    void Approve(int bookingId, string userId);
    void Reject(int bookingId, string userId);
}