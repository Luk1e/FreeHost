using AutoMapper;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Domain.Mapper.Converters;

public class StringToPhotoConverter : ITypeConverter<IEnumerable<string>, IEnumerable<Photo>>
{
    public IEnumerable<Photo> Convert(IEnumerable<string> source, IEnumerable<Photo> destination, ResolutionContext context)
    {
        var photos = source.ToList();
        var photosResult = new List<Photo>(photos.Count);
        photosResult.AddRange(photos.Select(photo => new Photo { Bytes = System.Convert.FromBase64String(photo) }));

        return photosResult;
    }
}