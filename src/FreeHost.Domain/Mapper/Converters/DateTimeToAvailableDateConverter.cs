using AutoMapper;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Domain.Mapper.Converters;

public class DateTimeToAvailableDateConverter : ITypeConverter<IEnumerable<DateTime>, IEnumerable<AvailableDate>>
{
    public IEnumerable<AvailableDate> Convert(IEnumerable<DateTime> source, IEnumerable<AvailableDate> destination, ResolutionContext context)
    {
        var dateTimes = source.ToList();
        var dates = new List<AvailableDate>(dateTimes.Count);
        dates.AddRange(dateTimes.Select(dateString => new AvailableDate { Date = dateString }));

        return dates;
    }
}