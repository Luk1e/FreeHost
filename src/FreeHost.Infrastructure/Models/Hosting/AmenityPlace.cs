using System.ComponentModel.DataAnnotations;

namespace FreeHost.Infrastructure.Models.Hosting;

public class AmenityPlace
{
    [Required]
    public int AmenitiesId { get; set; }

    [Required]
    public int PlacesId { get; set; }
}