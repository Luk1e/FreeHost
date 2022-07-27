using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;

namespace FreeHost.Infrastructure.Interfaces.Services;

public interface ISearchService
{
    SearchResponse FetchPlaces(SearchRequest request);
}