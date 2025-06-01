using SimpleBlog.Dtos.BlogPostDtos;
using SimpleBlog.Framework;

namespace SimpleBlog.Models
{
    public class BlogPost : IContent<int>
    {
        public BlogPost()
        {
            Title = string.Empty;
            CreateDate = DateTime.Now;
            ImageUrl = Shared.NoImage;
        }
        public int Id { get; set; }
        public bool ActiveVersion { get; set; }
        public bool Removed { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public string? Body { get; set; }
        public string? ImageUrl { get; set; }
        public int ContentId { get; set; }
        public virtual Content? Content { get; set; }
        public int CategoryId { get; set; }
        public virtual BlogCagtegory? Category { get; set; }
        public string AuthorId { get; set; }
        public User Author { get; set; }

        public DateTime GetDate()
        {
            return CreateDate;
        }

        public string GetDescription()
        {
            return Body ?? string.Empty;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetMainImage()
        {
            return ImageUrl ?? Shared.NoImage;
        }

        public string GetTitle()
        {
            return Title;
        }

        public string GetUrl()
        {
            return Content?.GetUrl()
                ?? throw new NullReferenceException("Related content not found");
        }

        public BlogPostDto AsDto()
        {
            return new BlogPostDto(GetId(), GetTitle(), GetDescription(), GetMainImage(), GetUrl(), CategoryId, Category?.GetTitle() ?? string.Empty);
        }
    }
}
