using FreeHost.Infrastructure.Models.Requests;

namespace FreeHost.Infrastructure.Interfaces.Services;

public interface IBookingService
{
    void Book(BookingRequest request, string userId);
    void Approve(int bookingId, string userId);
    void Reject(int bookingId, string userId);
}