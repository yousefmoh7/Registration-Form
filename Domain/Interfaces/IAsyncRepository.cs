using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        Task<T> GetAsyncById(int id);
        Task<T> GetAsyncById(int id, params Expression<Func<T, object>>[] includes);

        Task<List<T>> ListAsync();
        Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);

        Task<int> SaveChangesAsync();

    }
}