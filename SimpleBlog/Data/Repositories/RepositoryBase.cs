using Microsoft.EntityFrameworkCore;
using SimpleBlog.Framework;
using System.Linq.Expressions;

namespace SimpleBlog.Data.Repositories
{
    public abstract class RepositoryBase<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> DbSet;
        protected RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task DeleteAsync(TId id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
            }
        }

        public virtual async Task<TEntity> GetAsync(TId id)
        {
            var entity = await DbSet.FindAsync(id);
            return entity ?? throw new NullReferenceException();
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null)
        {
            var query = predicate == null ? DbSet : DbSet.Where(predicate);
            return query;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
