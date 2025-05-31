using Microsoft.EntityFrameworkCore;
using SimpleBlog.Framework;
using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Models
{
    public class Content : IContent<int>
    {
        public Content()
        {
            CreateDate = DateTime.Now;
        }
        public int Id { get; set; }

        private string token;
        [Required, StringLength(64)]
        public string Token
        {
            get { return token; }
            set { token = value?.Replace(' ', '-')?.Trim()?.ToLower() ?? string.Empty; }
        }

        public DateTime CreateDate { get; set; }

        public ContentEntityType EntityType { get; set; }
        public int EntityId { get; set; }

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
            return Token;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns a SEO ready url</returns>
        public string GetUrl()
        {
            return $"/{Token}";
        }
    }
}
