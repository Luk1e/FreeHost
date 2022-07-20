using System.Linq.Expressions;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FreeHost.Infrastructure.Database;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbFactory _dbFactory;
    private DbSet<T> _dbSet;

    private DbSet<T> DbSet => _dbSet ??= _dbFactory.AppDbContext.Set<T>();

    protected Repository(DbFactory dbFactory, IUnitOfWork unitOfWork)
    {
        _dbFactory = dbFactory;
        _unitOfWork = unitOfWork;
    }

    public virtual void Add(T entity)
    {
        DbSet.Add(entity);
        _unitOfWork.Commit();
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        DbSet.AddRange(entities);
        _unitOfWork.Commit();
    }

    public virtual void Delete(T entity)
    {
        DbSet.Remove(entity);
        _unitOfWork.Commit();
    }

    public virtual void DeleteRange(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
        _unitOfWork.Commit();
    }

    public virtual void Update(T entity)
    {
        DbSet.Update(entity);
        _unitOfWork.Commit();
    }

    public virtual IQueryable<T> Get(Expression<Func<T, bool>> expression)
    {
        return DbSet.Where(expression);
    }
}