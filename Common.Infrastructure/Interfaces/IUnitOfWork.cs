using Microsoft.EntityFrameworkCore;
namespace Common.Infrastructure.Interfaces
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        void Dispose(bool disposing);
        DbSet<T> Repository<T>() where T : class;
        void UpdateEntity<T>(T entity) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void beginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
    }
}
