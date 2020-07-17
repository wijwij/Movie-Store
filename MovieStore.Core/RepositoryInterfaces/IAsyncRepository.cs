using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        /*
         * ToDo [review - 7/15/2020]
         * Question on LINQ where method:
         *   in-memory data resource (IEnumerable) are passing Func<T, bool>
         *   out-memory data resource (IQueryable) are passing Expression<Func<T, bool>>
         * Why?
         *   Because LINQ needs to convert it to sql expressions later
         * ToDo [Follow-up Question: What's the translation process under the hood? How to check the converted SQL statements?]
         *
         * EF provides normal sync method and async method
         * .NET 4.5, c# 5
         *
         * async-await methods largely increase the performance for I/O intensive application
         * Naming convention of async method: ---Async
         */
        
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter);
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null);
        Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}