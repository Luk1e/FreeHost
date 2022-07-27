using FreeHost.Infrastructure.Models.DTOs;

namespace FreeHost.Infrastructure.Models.Responses;

public class SearchResponse
{
    public IEnumerable<PlaceSearchDto> Apartments { get; set; } = Enumerable.Empty<PlaceSearchDto>();
    public int CurrentPage { get; set; }
    public int MaxPage { get; set; }
}