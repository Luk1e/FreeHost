using AutoMapper;
using FreeHost.Infrastructure.Models.DTOs;
using FreeHost.Infrastructure.Models.Responses;
using FreeHost.Infrastructure.Models.Utils;

namespace FreeHost.Domain.Mapper.Converters;

public class PaginationSearchResponseConverter : ITypeConverter<PaginationResult<PlaceSearchDto>, SearchResponse>
{
    public SearchResponse Convert(PaginationResult<PlaceSearchDto> source, SearchResponse destination, ResolutionContext context)
    {
        return new SearchResponse{Apartments = source.Data, CurrentPage = source.CurrentPage, MaxPage = source.MaxPage};
    }
}