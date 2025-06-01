using SimpleBlog.Framework;
using System.Linq.Expressions;

namespace SimpleBlog.Data.Repositories
{
    public interface IRepository<T, TID> where T : IEntity<TID>
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(TID id);
        Task<T> GetAsync(TID id);
        IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null);

    }

    public interface IRepository<T> : IRepository<T, int> where T : IEntity<int>
    {
    }
}
