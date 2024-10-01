using System.Linq.Expressions;

namespace Common.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Insert(T entity);
        void InsertRange(IEnumerable<T> entity);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object id);
    }
}
