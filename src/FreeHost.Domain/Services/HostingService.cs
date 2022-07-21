using AutoMapper;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.DTOs;
using FreeHost.Infrastructure.Models.Hosting;
using FreeHost.Infrastructure.Models.Requests;

namespace FreeHost.Domain.Services;

public class HostingService : IHostingService
{
    private readonly IMapper _mapper;
    private readonly IPlaceRepo _placeRepo;
    private readonly IAmenityRepo _amenityRepo;
    private readonly ICityRepo _cityRepo;

    public HostingService(IMapper mapper, IPlaceRepo placeRepo, IAmenityRepo amenityRepo, ICityRepo cityRepo)
    {
        _mapper = mapper;
        _placeRepo = placeRepo;
        _amenityRepo = amenityRepo;
        _cityRepo = cityRepo;
    }

    public IEnumerable<PlaceDto> GetUserPlaces(string userId)
    {
        var places = _placeRepo.Get(x => x.User.Id == userId);
        var placeDTOs = new List<PlaceDto>(places.Count());
        foreach (var place in places)
        {
            var placeDTO = _mapper.Map<PlaceDto>(place);
            placeDTO.Owner = _mapper.Map<UserDto>(place.User);
            placeDTOs.Add(placeDTO);
        }
        return placeDTOs;
    }

    public PlaceDto GetUserPlace(int placeId, string userId)
    {
        var place = _placeRepo.Get(x => x.User.Id == userId && x.Id == placeId).SingleOrDefault() ??
                    throw new ArgumentNullException(nameof(Place), "Place not found");
        var placeDTO = _mapper.Map<PlaceDto>(place);
        placeDTO.Owner = _mapper.Map<UserDto>(place.User);

        return placeDTO;
    }

    public void AddPlace(AddPlaceRequest request, string userId) => _placeRepo.Add(_mapper.Map<Place>(request), userId);

    public void EditPlace(EditPlaceRequest request, string userId)
    {
        var place = _mapper.Map<Place>(request);
        _placeRepo.Update(place, userId);
    }

    public void DeletePlace(int placeId, string userId)
    {
        _placeRepo.Delete(placeId, userId);
    }

    public IEnumerable<string> GetAmenities()
    {
        return _amenityRepo.Get(x => !string.IsNullOrEmpty(x.Name)).Select(x => x.Name);
    }

    public IEnumerable<string> GetCities()
    {
        return _cityRepo.Get(x => !string.IsNullOrEmpty(x.Name)).Select(x => x.Name);
    }
}