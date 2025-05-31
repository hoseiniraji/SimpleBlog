using Microsoft.Extensions.Hosting;
using SimpleBlog.Framework;
using SimpleBlog.Models;

namespace SimpleBlog.Builders
{
    public class ContentBuilder
    {
        public BlogPostBuilder NewBlogPostBuilder()
        {
            return new BlogPostBuilder();
        }
        public BlogCategoryBuilder NewBlogCategoryBuilder()
        {
            return new BlogCategoryBuilder();
        }
        // nested classes
        public class BlogPostBuilder
        {
            BlogPost post;
            public BlogPostBuilder()
            {
                post = new BlogPost()
                {
                    Content = new Content()
                    {
                        EntityType = ContentEntityType.BlogPost,
                    }
                };

            }

            public BlogPostBuilder SetTitle(string title, string? token = null)
            {
                if (!string.IsNullOrEmpty(title))
                {
                    post.Title = title;
                    post.Content.Token = token ?? title;
                }

                return this;
            }

            public BlogPostBuilder SetBody(string body)
            {
                post.Body = body;
                return this;
            }

            public BlogPostBuilder SetImage(string imageUrl)
            {
                post.ImageUrl = imageUrl;
                return this;
            }



            public BlogPostBuilder SetId(int id)
            {
                post.Id = id;
                post.Content.EntityId = id;
                return this;
            }

            public BlogPostBuilder SetContentId(int contentId)
            {
                post.ContentId = contentId;
                post.Content.Id = contentId;
                return this;
            }

            public BlogPostBuilder SetCategoryId(int categoryId)
            {
                post.CategoryId = categoryId;
                return this;
            }

            public BlogPostBuilder SetActiveVersion(bool isActiveVersion)
            {
                post.ActiveVersion = isActiveVersion;
                return this;
            }

            public BlogPost Build()
            {
                return post;
            }

        }

        public class BlogCategoryBuilder
        {
            private BlogCagtegory category;
            public BlogCategoryBuilder()
            {
                category = new BlogCagtegory()
                {
                    Content = new Content()
                    {
                        EntityType = ContentEntityType.BLogCategory,
                    }
                };
            }

            public BlogCategoryBuilder SetTitle(string title, string? token = null)
            {
                if (!string.IsNullOrEmpty(title))
                {
                    category.Name = title;
                    category.Content.Token = token ?? title;
                }

                return this;
            }

            public BlogCategoryBuilder SetImage(string imageUrl)
            {
                category.ImageUrl = imageUrl;
                return this;
            }

            public BlogCategoryBuilder SetId(int id)
            {
                category.Id = id;
                category.Content.EntityId = id;
                return this;
            }

            public BlogCategoryBuilder SetContentId(int contentId)
            {
                category.ContentId = contentId;
                category.Content.Id = contentId;
                return this;
            }

            public BlogCagtegory Build()
            {
                return category;
            }

        }
    }
}
