using SimpleBlog.Framework;
using SimpleBlog.Models;

namespace SimpleBlog.Data.Repositories
{
    public interface IBlogPostRepository<TEntity, TId>
        :
        IContentRepository<TEntity, TId>,
        IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        public Task<IPagedList<TEntity>> GetPagedAsync(int p, int c, string q);
        public Task<string> SaveImage(IWebHostEnvironment _env, IFormFile image);
    }
}
