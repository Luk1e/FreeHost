namespace FreeHost.Infrastructure.Models.DTOs;

public class BookedDatesDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public UserDto Client { get; set; }
}