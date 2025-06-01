using Microsoft.EntityFrameworkCore;
using SimpleBlog.Framework;
using SimpleBlog.Models;

namespace SimpleBlog.Data.Repositories
{
    public class ContentRepository(ApplicationDbContext context, UnitOfWork unitOfWork) : RepositoryBase<Content, int>(context)
    {
        private readonly UnitOfWork _unitOfWork = unitOfWork;
        public async Task<IContent<int>> GetInfoByToken(string token)
        {
            var content = await DbSet.FirstOrDefaultAsync(x => x.Token == token)
                ?? throw new NullReferenceException("MainContent is null");

            switch (content.EntityType)
            {
                case ContentEntityType.BLogCategory:
                    return await _unitOfWork.BlogCategories.GetByContentIdAsync(content.Id);
                case ContentEntityType.BlogPost:
                    return await _unitOfWork.BlogPosts.GetByContentIdAsync(content.Id);

                case ContentEntityType.Unknown:
                default:
                    throw new FormatException("Invalid ContentEntityType");
            }
        }
    }
}
