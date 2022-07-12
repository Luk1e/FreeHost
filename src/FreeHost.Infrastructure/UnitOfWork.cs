using FreeHost.Infrastructure.Interfaces;

namespace FreeHost.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbFactory _dbFactory;

    public UnitOfWork(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public int Commit()
    {
        return _dbFactory.AppDbContext.SaveChanges();
    }
}