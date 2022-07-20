using AutoMapper;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Domain.Mapper.Converters;

public class AvailableDateToDateTimeConverter : ITypeConverter<IEnumerable<AvailableDate>, IEnumerable<DateTime>>
{
    public IEnumerable<DateTime> Convert(IEnumerable<AvailableDate> source, IEnumerable<DateTime> destination, ResolutionContext context)
    {
        return source.Select(x => x.Date);
    }
}