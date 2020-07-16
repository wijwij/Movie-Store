using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
         * ToDo [Follow-up Question: What's the translation process under the hood.]
         */
        
        T GetById(int id);
        IEnumerable<T> ListAll();
        IEnumerable<T> ListWhere(Expression<Func<T, bool>> where);
        int GetCount(Expression<Func<T, bool>> filter = null);
        bool GetExists(Expression<Func<T, bool>> filter = null);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}