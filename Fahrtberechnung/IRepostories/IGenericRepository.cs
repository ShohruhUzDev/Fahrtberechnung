using System.Linq.Expressions;

namespace Fahrtberechnung.IRepostories
{
    public interface IGenericRepository<T> 
    {
        ValueTask<T> CreateAsync(T entity);
        T Update(T entity);
        ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression,
            string[] includes = null,
            bool isTracking = true);
        ValueTask SaveChangesAsync();
    }
}
