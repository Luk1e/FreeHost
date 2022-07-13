namespace FreeHost.Infrastructure.Interfaces.Repositories;

public interface IUnitOfWork
{
    int Commit();
}