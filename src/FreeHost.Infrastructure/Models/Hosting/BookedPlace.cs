using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Enums;

namespace FreeHost.Infrastructure.Models.Hosting;

public class BookedPlace
{
    public int Id { get; set; }
    public Place Place { get; set; }
    public User Client { get; set; }
    public User Owner { get; set; }
    public BookingStatusEnum BookingStatus { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}