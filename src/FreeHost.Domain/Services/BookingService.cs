using System.Security.Cryptography.X509Certificates;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Enums;
using FreeHost.Infrastructure.Models.Hosting;
using FreeHost.Infrastructure.Models.Requests;

namespace FreeHost.Domain.Services;

public class BookingService : IBookingService
{
    private readonly IUserRepo _userRepo;
    private readonly IPlaceRepo _placeRepo;
    private readonly IBookedPlaceRepo _bookedPlaceRepo;

    public BookingService(IUserRepo userRepo, IPlaceRepo placeRepo, IBookedPlaceRepo bookedPlaceRepo)
    {
        _userRepo = userRepo;
        _placeRepo = placeRepo;
        _bookedPlaceRepo = bookedPlaceRepo;
    }

    public void Book(BookingRequest request, string userId)
    {
        var place = _placeRepo.Get(x => x.Id == request.ApartmentId).Single() ?? 
                    throw new ArgumentNullException(nameof(request.ApartmentId), "Apartment not found");

        if (place.User.Id == userId)
            throw new ArgumentException("Owners cannot book their own apartments");

        if (!place.BookedDates.Any(x => x.StartDate < request.EndDate == x.EndDate < request.StartDate && x.StartDate != request.EndDate)) 
            throw new ArgumentException("Could not book a place for this date range");

        var client = _userRepo.Get(x => x.Id == userId).Single() ??
                     throw new ArgumentNullException(nameof(userId), "Client user not found");

        var hasSameBookings = _bookedPlaceRepo
            .Get(x => x.Client.Id == userId)
            .Any(x => x.StartDate == request.StartDate && x.EndDate == request.EndDate);
        if (hasSameBookings)
            throw new ArgumentException("You've already booked this place for selected date range");

        var bookedPlace = new BookedPlace
        {
            BookingStatus = BookingStatusEnum.Waiting,
            Client = client,
            Owner = place.User,
            Place = place,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        _bookedPlaceRepo.Add(bookedPlace);
    }
}