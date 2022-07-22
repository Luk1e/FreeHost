using FreeHost.Infrastructure.Models.Authorization;

namespace FreeHost.Infrastructure.Models.Hosting;

public class BookedDate
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public User Client { get; set; }
}