using FreeHost.Infrastructure.Models.DTOs;
using FreeHost.Infrastructure.Models.Requests;

namespace FreeHost.Infrastructure.Interfaces.Services;

public interface IHostingService
{
    IEnumerable<PlaceDto> GetUserPlaces(string userId);
    void AddPlace(AddPlaceRequest request, string userId);
    void EditPlace(EditPlaceRequest request, string userId);
    void DeletePlace(int placeId, string userId);
}