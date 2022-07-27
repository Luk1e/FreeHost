using AutoMapper;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.DTOs;
using FreeHost.Infrastructure.Models.Enums;
using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;

namespace FreeHost.Domain.Services;

public class SearchService : ISearchService
{
    private const int PageLimit = 10;

    private readonly IPlaceRepo _placeRepo;
    private readonly IMapper _mapper;

    public SearchService(IPlaceRepo placeRepo, IMapper mapper)
    {
        _placeRepo = placeRepo;
        _mapper = mapper;
    }

    public SearchResponse FetchPlaces(SearchRequest request)
    {
        var places = _placeRepo
            .Get(x => x.City.Name.ToLower() == request.City.ToLower())
            .Select(x => _mapper.Map<PlaceSearchDto>(x)).ToList();

        places = ApplyFiltering(places, request.City, request.NumberOfBeds);

        places = SetAvailability(places, request.StartDate, request.EndDate);

        places = ApplySorting(places, request.SortBy);

        var result = ApplyPagination(places, request.Page);

        return result;
    }

    private static List<PlaceSearchDto> ApplyFiltering(IEnumerable<PlaceSearchDto> places, string City, NumberOfBedsEnum numberOfBeds)
    {
        if (numberOfBeds != 0 && numberOfBeds <= NumberOfBedsEnum.ThreePlus)
        {
            places = numberOfBeds == NumberOfBedsEnum.ThreePlus
                ? places.Where(x => x.NumberOfBeds >= 3)
                : places.Where(x => x.NumberOfBeds == (int)numberOfBeds);
        }

        return places.ToList();
    }

    private static List<PlaceSearchDto> SetAvailability(IEnumerable<PlaceSearchDto> places, DateTime StartDate, DateTime EndDate)
    {
        var apartments = places.ToList();

        foreach (var place in apartments.Where(place =>
                     place.BookedDates.Any(y => y.StartDate < EndDate) ==
                     place.BookedDates.Any(y => y.EndDate < StartDate)))
        {
            if (place.BookedDates.Any(y => y.StartDate == EndDate))
                continue;
            place.Available = true;
        }

        return apartments;
    }

    private static List<PlaceSearchDto> ApplySorting(IEnumerable<PlaceSearchDto> places, SortByEnum sortBy)
    {
        places = sortBy switch
        {
            SortByEnum.BedsAscending => places.OrderBy(x => x.NumberOfBeds),
            SortByEnum.BedsDescending => places.OrderByDescending(x => x.NumberOfBeds),
            SortByEnum.DistanceAscending => places.OrderBy(x => x.DistanceFromTheCenter),
            SortByEnum.DistanceDescending => places.OrderByDescending(x => x.DistanceFromTheCenter),
            _ => places.OrderBy(x => x.NumberOfBeds)
        };
        places = places.OrderByDescending(x => x.Available);

        return places.ToList();
    }

    private static SearchResponse ApplyPagination(IEnumerable<PlaceSearchDto> places, int currentPage)
    {
        var apartments = places.ToList();

        if (currentPage < 1)
            currentPage = 1;
        var maxPage = (int)Math.Ceiling(apartments.Count / (double)PageLimit);
        if (currentPage > maxPage)
            currentPage = maxPage;

        var skippedItems = PageLimit * currentPage - PageLimit;
        apartments = apartments.Skip(skippedItems).Take(PageLimit).ToList();

        var result = new SearchResponse { Apartments = apartments, CurrentPage = currentPage, MaxPage = maxPage };

        return result;
    }
}