using SimpleBlog.Framework;

namespace SimpleBlog.Data.Repositories
{
    public interface IContentRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        Task<TEntity> GetByContentIdAsync(TId contentId);
    }
}
