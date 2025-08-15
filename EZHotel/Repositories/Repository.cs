using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EZHotel.Data;
using EZHotel.Repositories.IRepositories;

namespace EZHotel.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _db;

        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        #region CRUD Operations

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public void DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        #endregion

        #region Retrieve Data

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<TFilter>(TFilter? filter = default
            , Func<TFilter, Expression<Func<T, bool>>>? filterBuilder = null, params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filterBuilder != null && filter != null)
            {
                var predicate = filterBuilder(filter);
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        #endregion

        #region Check

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbSet;
            return await query.AnyAsync(predicate);
        }

        #endregion
    }
}
