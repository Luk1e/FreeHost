using System.Linq.Expressions;

namespace FreeHost.Infrastructure.Interfaces.Database;

public interface IRepository<T> where T : class
{
    /// <summary>
    /// Do NOT use this method to Add a Place!
    /// </summary>
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);

    /// <summary>
    /// Do NOT use this method to Delete a Place! Unless used by admins.
    /// </summary>
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);

    /// <summary>
    /// Do NOT use this method to Update a Place!
    /// </summary>
    void Update(T entity);
    IQueryable<T> Get(Expression<Func<T, bool>> expression);
}