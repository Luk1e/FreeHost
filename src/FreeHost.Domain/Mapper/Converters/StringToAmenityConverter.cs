using AutoMapper;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Domain.Mapper.Converters;

public class StringToAmenityConverter : ITypeConverter<IEnumerable<string>, IEnumerable<Amenity>>
{
    public IEnumerable<Amenity> Convert(IEnumerable<string> source, IEnumerable<Amenity> destination, ResolutionContext context)
    {
        var amenities = source.ToList();
        var amenitiesResult = new List<Amenity>(amenities.Count);
        amenitiesResult.AddRange(amenities.Select(sourceAmenity => new Amenity { Name = sourceAmenity }));

        return amenitiesResult;
    }
}