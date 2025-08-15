using System.Linq.Expressions;

namespace EZHotel.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync<TFilter>(TFilter? filter = default, Func<TFilter, Expression<Func<T, bool>>>? filterBuilder = null, params Expression<Func<T, object>>[]? includes);
        Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void RemoveRangeAsync(IEnumerable<T> entities);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
