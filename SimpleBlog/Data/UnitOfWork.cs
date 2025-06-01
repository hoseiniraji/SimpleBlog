using SimpleBlog.Data.Repositories;

namespace SimpleBlog.Data
{
    public class UnitOfWork(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        #region [- BlogCategories -]
        private BlogCategoryRepository? blogCategories;

        public BlogCategoryRepository? BlogCategories
        {
            get
            {
                blogCategories ??= new BlogCategoryRepository(_context);
                return blogCategories;
            }
        }
        #endregion

        #region [- BlogPosts -]
        private BlogPostRepository? blogPosts;

        public BlogPostRepository? BlogPosts
        {
            get
            {
                blogPosts ??= new BlogPostRepository(_context);
                return blogPosts;
            }
        }

        #endregion

        #region [- Contents -]

        private ContentRepository? contents;

        public ContentRepository? Contents
        {
            get
            {
                contents ??= new ContentRepository(_context, this);
                return contents;
            }
        }

        #endregion
    }
}
