using Common.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly DbContext _dbContext;
        private bool _disaposed = false;
        private Dictionary<Type, object> _repositories;
        private DatabaseFacade _databaseFacadeTransaction;

        public UnitOfWork(TContext context)
        {
            _dbContext = context;
        }      

        public DbSet<T> Repository<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public void beginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disaposed)
            {
                // clear repositories
                if (_repositories != null)
                {
                    _repositories.Clear();
                }
            }
            _disaposed = true;
        }

        public void RollBackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void UpdateEntity<T>(T entity) where T : class
        {
            _dbContext.Update(entity);
        }
    }
}
