namespace FreeHost.Infrastructure.Interfaces;

public interface IUnitOfWork
{
    int Commit();
}