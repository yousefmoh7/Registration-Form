using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructre.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;
        private readonly EFContext _dbContext;

        public RepositoryBase(EFContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.FromResult(true);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefaultAsync(expression);
        }

        //public async Task<T> GetAsyncById(int id, string[] paths = null)
        //{
        //    var model = await _dbSet.FindAsync(id);
        //    foreach (var path in paths)
        //    {
        //        _dbContext.Entry(model).Reference(path).Load();
        //    }
        //    return model;
        //}

        public async Task<T> GetAsyncById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetAsyncById(int id, params Expression<Func<T, object>>[] includes)
        {
            includes.ToList().ForEach(x => _dbSet.Include(x).Load());
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToListAsync();
        }
        public Task<List<T>> ListAsync()
        {
            return _dbSet.ToListAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }
    }
}