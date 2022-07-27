using System.ComponentModel.DataAnnotations;
using FreeHost.Infrastructure.Models.Enums;

namespace FreeHost.Infrastructure.Models.Requests;

public class SearchRequest
{
    [Required]
    public string City { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public NumberOfBedsEnum NumberOfBeds { get; set; }

    public SortByEnum SortBy { get; set; }

    public int Page { get; set; } = 1;
}