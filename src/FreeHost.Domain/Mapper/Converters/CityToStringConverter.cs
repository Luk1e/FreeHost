using AutoMapper;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Domain.Mapper.Converters;

public class CityToStringConverter : ITypeConverter<City, string>
{
    public string Convert(City source, string destination, ResolutionContext context)
    {
        return source.Name;
    }
}