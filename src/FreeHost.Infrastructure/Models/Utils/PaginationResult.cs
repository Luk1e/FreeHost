namespace FreeHost.Infrastructure.Models.Utils;

public class PaginationResult<T>
{
    public IEnumerable<T> Data { get; set; }
    public int CurrentPage { get; set; }
    public int MaxPage { get; set; }
}