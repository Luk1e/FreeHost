using AutoMapper;
using FreeHost.Domain.Utils;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.DTOs;
using FreeHost.Infrastructure.Models.Enums;
using FreeHost.Infrastructure.Models.Hosting;
using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;

namespace FreeHost.Domain.Services;

public class BookingService : IBookingService
{
    private readonly IUserRepo _userRepo;
    private readonly IPlaceRepo _placeRepo;
    private readonly IBookedPlaceRepo _bookedPlaceRepo;
    private readonly IMapper _mapper;

    public BookingService(IUserRepo userRepo, IPlaceRepo placeRepo, IBookedPlaceRepo bookedPlaceRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _placeRepo = placeRepo;
        _bookedPlaceRepo = bookedPlaceRepo;
        _mapper = mapper;
    }

    public BookingResponse GetBookings(string userId, int page)
    {
        var bookedPlaces = _bookedPlaceRepo.Get(x => x.Client.Id == userId).OrderBy(x => x.BookingStatus);
        var pagination = Paginator.ApplyPagination(bookedPlaces, page);
        var response = _mapper.Map<BookingResponse>(pagination.Data);
        response.Bookings = _mapper.Map<IEnumerable<BookingDto>>(bookedPlaces);

        return response;
    }

    public BookingResponse GetGuests(string userId, int page)
    {
        var guests = _bookedPlaceRepo.Get(x => x.Owner.Id == userId).OrderBy(x => x.BookingStatus);
        var pagination = Paginator.ApplyPagination(guests, page);
        var response = _mapper.Map<BookingResponse>(pagination.Data);
        response.Bookings = _mapper.Map<IEnumerable<BookingDto>>(guests);

        return response;
    }

    public void Book(BookingRequest request, string userId)
    {
        if (request.EndDate < request.StartDate)
            throw new ArgumentException("Check-out date cannot be earlier than check-in");

        if (request.StartDate < DateTime.UtcNow)
            throw new ArgumentException("Cannot book a date today or in the past");

        var place = _placeRepo.Get(x => x.Id == request.ApartmentId).SingleOrDefault() ?? 
                    throw new ArgumentNullException(nameof(request.ApartmentId), "Apartment not found");

        if (place.User.Id == userId)
            throw new ArgumentException("Owners cannot book their own apartments");

        if (!place.BookedDates.Any(x => x.StartDate < request.EndDate == x.EndDate < request.StartDate && x.StartDate != request.EndDate) && place.BookedDates.Any()) 
            throw new ArgumentException("Could not book a place for this date range");

        var client = _userRepo.Get(x => x.Id == userId).SingleOrDefault() ??
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

    public void Approve(int bookingId, string userId)
    {
        var booking = _bookedPlaceRepo.Get(x => x.Id == bookingId).SingleOrDefault() ??
                      throw new ArgumentNullException(nameof(bookingId), "Booking not found");

        if (booking.BookingStatus == BookingStatusEnum.Accepted)
            throw new ArgumentException("This booking is already accepted");

        if (booking.Owner.Id != userId)
            throw new ArgumentException("Only owners can approve bookings for their places");

        var place = _placeRepo.Get(x => x.Id == booking.Place.Id).SingleOrDefault() ??
                    throw new ArgumentNullException(nameof(bookingId), "Place for this booking is not found");

        if (place.BookedDates.Any(x => x.StartDate < booking.EndDate == x.EndDate < booking.StartDate && x.StartDate != booking.EndDate))
            throw new ArgumentException("Another booking for this date range is already approved");

        booking.BookingStatus = BookingStatusEnum.Accepted;
        _bookedPlaceRepo.Update(booking);

        if (!place.BookedDates.Any(x => x.Client == booking.Client && x.EndDate == booking.EndDate && x.StartDate == booking.StartDate))
        {
            var newBookedDates = place.BookedDates.ToList();
            newBookedDates.Add(new BookedDate { Client = booking.Client, EndDate = booking.EndDate, StartDate = booking.StartDate });
            place.BookedDates = newBookedDates;
            _placeRepo.Update(place);
        }

        var rejected = _bookedPlaceRepo.Get(x => x.Place == place)
            .Where(x => x.StartDate >= booking.EndDate == x.EndDate >= booking.StartDate && x.BookingStatus == BookingStatusEnum.Waiting).ToList();
        foreach (var bookedPlace in rejected)
        {
            bookedPlace.BookingStatus = BookingStatusEnum.Rejected;
            _bookedPlaceRepo.Update(bookedPlace);
        }
    }

    public void Reject(int bookingId, string userId)
    {
        var booking = _bookedPlaceRepo.Get(x => x.Id == bookingId).SingleOrDefault() ??
                      throw new ArgumentNullException(nameof(bookingId), "Booking not found");

        if (booking.BookingStatus == BookingStatusEnum.Accepted)
            throw new ArgumentException("This booking is already rejected");

        if (booking.Owner.Id != userId)
            throw new ArgumentException("Only owners can reject bookings for their places");

        booking.BookingStatus = BookingStatusEnum.Rejected;
        _bookedPlaceRepo.Update(booking);
    }
}