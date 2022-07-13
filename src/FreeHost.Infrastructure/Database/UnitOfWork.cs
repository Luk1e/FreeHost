using FreeHost.Infrastructure.Interfaces.Repositories;

namespace FreeHost.Infrastructure.Database;

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