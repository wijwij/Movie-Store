using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Helpers;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T: class
    {
        protected readonly MoviesStoreDbContext _dbContext;

        public EfRepository(MoviesStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // ToDo [Question - Why use virtual keyword?]
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<PaginatedList<T>> GetPagedResultAsync(int pageIndex, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderQuery = null, Expression<Func<T, bool>> filter = null)
        {
            return await PaginatedList<T>.GetPagedList(_dbContext.Set<T>(), pageIndex, pageSize, orderQuery, filter);
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            return filter != null
                ? await _dbContext.Set<T>().Where(filter).CountAsync()
                : await _dbContext.Set<T>().CountAsync();
        }

        public virtual async Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            return filter != null && await _dbContext.Set<T>().Where(filter).AnyAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            // save to the database
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            if (entity == null) return 0;
            _dbContext.Set<T>().Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}