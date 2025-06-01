using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;
using System.Linq.Expressions;

namespace SimpleBlog.Data.Repositories
{
    public class BlogCategoryRepository(ApplicationDbContext context) 
        : RepositoryBase<BlogCagtegory, int>(context), IContentRepository<BlogCagtegory, int>
    {
        public async Task<BlogCagtegory> GetByContentIdAsync(int contentId)
        {
            var result = await GetAllAsync(c => c.ContentId == contentId).FirstOrDefaultAsync();
            return result ?? throw new NullReferenceException();
        }
    }
}
