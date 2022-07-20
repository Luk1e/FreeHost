using AutoMapper;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Domain.Mapper.Converters;

public class StringToCityConverter : ITypeConverter<string, City>
{
    public City Convert(string source, City destination, ResolutionContext context)
    {

        return new City{ Name = source };
    }
}