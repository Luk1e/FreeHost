using AutoMapper;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Domain.Mapper.Converters;

public class DateTimeToBookedDateConverter : ITypeConverter<IEnumerable<DateTime>, IEnumerable<BookedDate>>
{
    public IEnumerable<BookedDate> Convert(IEnumerable<DateTime> source, IEnumerable<BookedDate> destination, ResolutionContext context)
    {
        var dateTimes = source.ToList();
        var dates = new List<BookedDate>(dateTimes.Count);
        dates.AddRange(dateTimes.Select(dateString => new BookedDate { Date = dateString }));

        return dates;
    }
}