using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EllyShopContext _dbContext;
        private DbSet<T> _dbSet;

        public GenericRepository(EllyShopContext context)
        {
            this._dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Insert(T entity)
        {
            _dbSet.AddAsync(entity);
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                Insert(item);
            }
        }

        public async void Delete(T entity)
        {
            T existing = await _dbSet.FindAsync(entity);
            _dbSet.Remove(existing);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public void Update(T obj)
        {
            _dbSet.Attach(obj);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.UpdateRange(entities);
        }

    }
}
