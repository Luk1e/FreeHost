using FreeHost.Infrastructure.Models.Requests;

namespace FreeHost.Infrastructure.Interfaces.Services;

public interface IBookingService
{
    void Book(BookingRequest request, string userId);
}