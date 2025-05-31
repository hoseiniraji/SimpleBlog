using SimpleBlog.Framework;
using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Models
{
    public class BlogCagtegory : IContent<int>
    {
        public BlogCagtegory()
        {
            CreateDate = DateTime.Now;
            Name = string.Empty;
            ImageUrl = Shared.NoImage;
        }
        public int Id { get; set; }
        [Required, StringLength(32)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }

        public int ContentId { get; set; }
        public virtual Content? Content { get; set; }
        public virtual ICollection<BlogPost> Posts { get; set; }
        public DateTime GetDate()
        {
            return CreateDate;
        }

        public string GetDescription()
        {
            return string.Empty;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetMainImage()
        {
            return Shared.NoImage;
        }

        public string GetTitle()
        {
            return Name;
        }

        public string GetUrl()
        {
            return Content?.GetUrl()
                ?? throw new NullReferenceException("Related content not found");
        }
    }
}
