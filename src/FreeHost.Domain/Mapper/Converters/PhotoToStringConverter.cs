using AutoMapper;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Domain.Mapper.Converters;

public class PhotoToStringConverter : ITypeConverter<IEnumerable<Photo>, IEnumerable<string>>
{
    public IEnumerable<string> Convert(IEnumerable<Photo> source, IEnumerable<string> destination, ResolutionContext context)
    {
        return source.Select(x => System.Convert.ToBase64String(x.Bytes));
    }
}