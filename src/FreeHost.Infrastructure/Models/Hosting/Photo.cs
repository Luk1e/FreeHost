namespace FreeHost.Infrastructure.Models.Hosting;

public class Photo
{
    public int Id { get; set; }
    public byte[] Bytes { get; set; }
    public int PlaceId { get; set; }
}