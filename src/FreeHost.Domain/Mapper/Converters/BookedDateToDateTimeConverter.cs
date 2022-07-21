using AutoMapper;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Domain.Mapper.Converters;

public class BookedDateToDateTimeConverter : ITypeConverter<IEnumerable<BookedDate>, IEnumerable<DateTime>>
{
    public IEnumerable<DateTime> Convert(IEnumerable<BookedDate> source, IEnumerable<DateTime> destination, ResolutionContext context)
    {
        return source.Select(x => x.Date);
    }
}