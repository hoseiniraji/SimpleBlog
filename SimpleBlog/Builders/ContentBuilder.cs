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

                };

            }

            public BlogPostBuilder SetTitle(string title, string? token = null)
            {
                if (!string.IsNullOrEmpty(title))
                {
                    post.Title = title;
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
                return this;
            }

            public BlogPostBuilder SetContentId(int contentId)
            {
                post.ContentId = contentId;
                return this;
            }

            public BlogPostBuilder SetAuthorId(string authorId)
            {
                post.AuthorId = authorId;
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
                };
            }

            public BlogCategoryBuilder SetTitle(string title, string? token = null)
            {
                if (!string.IsNullOrEmpty(title))
                {
                    category.Name = title;
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
                return this;
            }

            public BlogCategoryBuilder SetContentId(int contentId)
            {
                category.ContentId = contentId;
                return this;
            }

            public BlogCagtegory Build()
            {
                return category;
            }

        }
    }
}
