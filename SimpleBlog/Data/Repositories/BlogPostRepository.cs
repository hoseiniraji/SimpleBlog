using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;
using System.Linq.Expressions;

namespace SimpleBlog.Data.Repositories
{
    public class BlogPostRepository(ApplicationDbContext context)
        : RepositoryBase<BlogPost, int>(context), IContentRepository<BlogPost, int>
    {
        public override IQueryable<BlogPost> GetAll(Expression<Func<BlogPost, bool>>? predicate = null)
        {
            return 
                base.GetAllAsync(predicate)
                    .Include(p => p.Category);
        }

        public override async Task<BlogPost> GetAsync(int id)
        {
            return 
                await this.GetAllAsync(c => c.Id == id).FirstOrDefaultAsync() 
                ?? throw new NullReferenceException();
        }

        public async Task<BlogPost> GetByContentIdAsync(int contentId)
        {
            // ensure fetch active version only
            var result = await this.GetAllAsync(c => c.ContentId == contentId && c.ActiveVersion).FirstOrDefaultAsync();
            return result ?? throw new NullReferenceException();
        }


    }
}
