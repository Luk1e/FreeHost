using AutoMapper;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Domain.Mapper.Converters;

public class AmenityToStringConverter : ITypeConverter<IEnumerable<Amenity>, IEnumerable<string>>
{
    public IEnumerable<string> Convert(IEnumerable<Amenity> source, IEnumerable<string> destination, ResolutionContext context)
    {
        return source.Select(x => x.Name);
    }
}