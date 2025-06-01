using Humanizer;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Builders;
using SimpleBlog.Dtos.BlogPostDtos;
using SimpleBlog.Framework;
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
                base.GetAll(predicate)
                    .Include(p => p.Content)
                    .Include(p => p.Author)
                    .Include(p => p.Category);
        }

        public override async Task<BlogPost> GetAsync(int id)
        {
            return
                await
                this.GetAll(c => c.Id == id)
                    .Include(p => p.Category)
                    .ThenInclude(pc => pc.Content)
                    .FirstOrDefaultAsync()
                ?? throw new NullReferenceException();
        }

        public async Task<BlogPost> GetByContentIdAsync(int contentId)
        {
            // ensure fetch active version only
            var result = await this
                .GetAll(c => c.ContentId == contentId && c.ActiveVersion)
                .Include(p => p.Category)
                .ThenInclude(pc => pc.Content)
                .FirstOrDefaultAsync();
            return result ?? throw new NullReferenceException();
        }

        public async Task<PageList<BlogPost>> GetPagedAsync(int p, int c, string q)
        {
            var query = GetAll(p => p.ActiveVersion && (string.IsNullOrEmpty(q) || p.Title.Contains(q)));
            var allItemsCount = await query.CountAsync();
            var items = await query
                            .OrderByDescending(p => p.Id)
                            .Skip((p - 1) * c)
                            .Take(c)
                            .ToListAsync();

            var result = new PageList<BlogPost>(items, p, c, allItemsCount, q);
            return result;

        }

        public async Task<BlogPost> UpdateAsync(UpdateBlogPostDto dto, IWebHostEnvironment _env, IFormFile? image)
        {
            var oldVersion = await GetAsync(dto.Id);
            var activeVersions = await GetAll(p => p.ContentId == oldVersion.ContentId && p.ActiveVersion).ToListAsync();

            // disable others
            activeVersions.ForEach(p => p.ActiveVersion = false);

            // create new version
            var builder = new ContentBuilder()
                    .NewBlogPostBuilder()
                    .SetTitle(dto.Title)
                    .SetBody(dto.Body)
                    .SetCategoryId(dto.CategoryId)
                    .SetActiveVersion(true)
                    .SetContentId(oldVersion.ContentId)
                    .SetAuthorId(oldVersion?.AuthorId);

            if (image != null)
            {
                string imagePath = await SaveImage(_env, image);
                builder.SetImage(imagePath);
            }
            else
            {
                builder.SetImage(oldVersion.GetMainImage());
            }
            var post = builder.Build();
            await CreateAsync(post);

            return post;
        }

        public override async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                var allVersions = await GetAll(p => p.ContentId == entity.ContentId).ToListAsync();
                allVersions.ForEach(p => p.Removed = true);
            }
        }

        public async Task<string> SaveImage(IWebHostEnvironment _env, IFormFile image)
        {
            string imageUrl = await FileHelper.SaveUploadedFileAsync(_env, image, Shared.BlogImageUploadPath);
            return imageUrl;
        }

        public async Task UpdateActiveVersion(int id)
        {
            var currentVersion = await GetAsync(id);
            var activeVersions = await GetAll(p => p.ContentId == currentVersion.ContentId && p.ActiveVersion).ToListAsync();

            activeVersions.ForEach(p => p.ActiveVersion = false);
            currentVersion.ActiveVersion = true;

        }
    }
}
