using System.ComponentModel.DataAnnotations;

namespace FreeHost.Infrastructure.Models.Requests;

public class AddPlaceRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public int? NumberOfBeds { get; set; }

    [Required]
    [MaxLength(5)]
    public IEnumerable<string> Amenities { get; set; }

    [Required]
    public IEnumerable<string> Photos { get; set; }

    [Required]
    public int? DistanceFromTheCenter { get; set; }
}