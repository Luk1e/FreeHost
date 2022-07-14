using System.Linq.Expressions;

namespace FreeHost.Infrastructure.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Delete(T entity);
    void Update(T entity);
    IQueryable<T> Get(Expression<Func<T, bool>> expression);
}