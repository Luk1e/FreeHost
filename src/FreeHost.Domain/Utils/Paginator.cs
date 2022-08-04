using FreeHost.Infrastructure.Models.Utils;

namespace FreeHost.Domain.Utils;

public static class Paginator
{
    private const int PageLimit = 5;

    public static PaginationResult<T> ApplyPagination<T>(IEnumerable<T> source, int currentPage)
    {
        var sources = source.ToList();

        if (currentPage < 1)
            currentPage = 1;
        var maxPage = (int)Math.Ceiling(sources.Count / (double)PageLimit);
        if (currentPage > maxPage)
            currentPage = maxPage;

        var skippedItems = PageLimit * currentPage - PageLimit;
        var result = sources.Skip(skippedItems).Take(PageLimit);

        return new PaginationResult<T>{ Data = result, CurrentPage = currentPage, MaxPage = maxPage };
    }
}