using System.ComponentModel.DataAnnotations;

namespace FreeHost.Infrastructure.Models.Requests;

public class BookingRequest
{
    [Required]
    public int ApartmentId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
}