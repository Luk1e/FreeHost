namespace FreeHost.Infrastructure.Database;

public class DbFactory
{
    private readonly Func<AppDbContext> _instanceFunc;
    private AppDbContext _appDbContext;
    public AppDbContext AppDbContext => _appDbContext ??= _instanceFunc.Invoke();

    public DbFactory(Func<AppDbContext> dbContextFactory)
    {
        _instanceFunc = dbContextFactory;
    }
}